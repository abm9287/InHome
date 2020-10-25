﻿using In_Home.Areas.Users.Models;
using In_Home.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace In_Home.Library
{
    public class LUser : ListObject
    {
        public LUser
            (UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _usersRole = new LUsersRoles();
        }
        //Método
        public async Task<List<InputModelRegister>> getTUsuariosAsync(String valor, int id)
        {
            List<TUsers> listUser;
            List<SelectListItem> _listRoles;
            List<InputModelRegister> userList = new List<InputModelRegister>();
            if (valor == null && id.Equals(0))
            {
                listUser = _context.TUsers.ToList();
            }
            else
            {
                if(id.Equals(0))
                {
                    listUser = _context.TUsers.Where(u => u.CI.StartsWith(valor) || u.Name.StartsWith(valor) ||
                    u.LastName.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();
                }
                else
                {
                    listUser = _context.TUsers.Where(u => u.ID.Equals(id)).ToList();
                }
            }
            if(!listUser.Count.Equals(0))
            {
                foreach(var item in listUser)
                {
                    _listRoles = await _usersRole.getRole(_userManager, _roleManager, item.IdUser);
                    var user = _context.Users.Where(u => u.Id.Equals(item.IdUser)).ToList().Last();
                    userList.Add(new InputModelRegister 
                    {
                        Id = item.ID,
                        ID = item.IdUser,
                        CI = item.CI,
                        Name = item.Name,
                        LastName = item.LastName,
                        Email = item.Email,
                        Role = _listRoles[0].Text,
                        Image = item.Image,
                        IdentityUser = user
                    });
                    _listRoles.Clear();
                }
            }
            return userList;
        }
    }
}
