using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In_Home.Areas.Users.Models;
using In_Home.Data;
using In_Home.Library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace In_Home.Areas.Users.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private LUsersRoles _usersRole;
        private static InputModel _dataInput;
        private Uploadimage _uploadimage; 
        private static InputModelRegister _dataUser1, _dataUser2;
        private IWebHostEnvironment _environment;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _environment = environment;
            _usersRole = new LUsersRoles();
            _uploadimage = new Uploadimage();

        }
        public void OnGet()
        {
            if (_dataInput != null)
            {
                Input = _dataInput;
                Input.rolesLista = _usersRole.getRoles(_roleManager);
                Input.AvatarImage = null;
            }
            else
            {
                Input = new InputModel
                {
                    rolesLista = _usersRole.getRoles(_roleManager)
                };
            }
            if (_dataUser1 != null)
            {
                Input = new InputModel
                {
                    Name = _dataUser1.Name,
                    LastName = _dataUser1.LastName,
                    CI = _dataUser1.CI,
                    Email = _dataUser1.Email,
                    Image = _dataUser1.Image,
                    PhoneNumber = _dataUser1.IdentityUser.PhoneNumber,
                    rolesLista = getRoles(_dataUser1.Role),

                };
            }
            _dataUser2 = _dataUser1;
            _dataUser1 = null;

        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {
            public IFormFile AvatarImage { get; set; }
            [TempData]
            public string ErrorMessage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
        public async Task<IActionResult> OnPost(String dataUser)
        {
            if (dataUser == null)
            {

                if (await SaveAsync())
                {
                    return Redirect("/Users/Users?area=Users");
                }
                else
                {
                    return Redirect("/Users/Register");
                }
            }
            else
            {
                _dataUser1 = JsonConvert.DeserializeObject<InputModelRegister>(dataUser);
                return Redirect("/Users/Register");
            }

        }
        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            var valor = false;
            if(ModelState.IsValid)
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if(userList.Count.Equals(0))
                {
                    var strategy = _context.Database.CreateExecutionStrategy();
                    await strategy.ExecuteAsync(async () => { 
                        using(var transaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                var user = new IdentityUser
                                {
                                    UserName = Input.Email,
                                    Email = Input.Email,
                                    PhoneNumber = Input.PhoneNumber
                                };
                                var result = await _userManager.CreateAsync(user, Input.Password);
                                if(result.Succeeded)
                                {
                                    await _userManager.AddToRoleAsync(user, Input.Role);
                                    var dataUser = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList().Last();
                                    var imageByte = await _uploadimage.ByteAvatarImageAsync(Input.AvatarImage, _environment, "images/images/user1.png");
                                    var t_user = new TUsers
                                    {
                                        Name = Input.Name,
                                        LastName = Input.LastName,
                                        CI = Input.CI,
                                        Email = Input.Email,
                                        IdUser = dataUser.Id,
                                        Image = imageByte,
                                    };
                                    await _context.AddAsync(t_user);
                                    _context.SaveChanges();

                                    transaction.Commit();
                                    _dataInput = null;
                                    valor = true;

                                }
                                else
                                {
                                    foreach(var item in result.Errors)
                                    {
                                        _dataInput.ErrorMessage = item.Description;

                                    }
                                    valor = false;
                                    transaction.Rollback();
                                }
                            }
                            catch(Exception ex)
                            {
                                _dataInput.ErrorMessage = ex.Message;
                                transaction.Rollback();
                                valor = false;
                            }
                        }
                    });
                }
                else
                {
                    foreach(var modelState in ModelState.Values)
                    {
                        foreach(var error in modelState.Errors)
                        {
                            _dataInput.ErrorMessage += error.ErrorMessage;
                        }
                    }
                    _dataInput.ErrorMessage = $"The  {Input.Email} is registered ";
                    valor = false;
                }
            }
            else
            {
                valor = false;
            }
            return valor;
        }
        private List<SelectListItem> getRoles(String role)
        {
            List<SelectListItem> rolesLista = new List<SelectListItem>();
            rolesLista.Add(new SelectListItem 
            {
                Text = role
            });
            var roles = _usersRole.getRoles(_roleManager);
            roles.ForEach(item => {
                if (item.Text != role)
                {
                    rolesLista.Add(new SelectListItem 
                    {
                        Text = item.Text
                    });
                }
            });
            return roles;
        }
        private async Task<bool> UpdateAsync()
        {
            var valor = false;
            byte[] imageByte = null;
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () => {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var identityUser = _userManager.Users.Where(u => u.Id.Equals(_dataUser2.ID)).ToList().Last();
                        identityUser.UserName = Input.Email;
                        identityUser.Email = Input.Email;
                        identityUser.PhoneNumber = Input.PhoneNumber;
                        _context.Update(identityUser);
                        await _context.SaveChangesAsync();

                    }
                    catch(Exception)
                    {
                        throw;
                    }
                }
            });
            return valor;
        }
    }
}
