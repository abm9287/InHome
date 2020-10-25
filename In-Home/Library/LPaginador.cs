using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace In_Home.Library
{
    public class LPaginador<T>
    {
        /*Atributos
        Cantidad de resultados o registros por página  */
        private int pagina_cuantos = 8;
        //Cantidad de enlaces que se mostrarán como máximo en la barra de navegación 'enlaces numéricos que se visualizarán
        private int pagina_nav_num_enlaces = 3;
        private int pagina_actual; //Atributo que contendrá en # de páginas
        //Definimos que irá en el enlace a la página anterior 
        private String pagina_nav_anterior = "&laquo; Anterior";
        //Definimos que irá en el enlace a la siguiente página
        private String pagina_nav_siguiente = "Siguiente &raqueo;";
        //Se define que irá en el enlace a la página siguiente
        private String pagina_nav_primera = "&laquo; Primero";
        private String pagina_nav_ultima = "Último &raquo;";
        private String pagina_navegacion = null;

        //Método
        public object[] paginador(List<T> table, int pagina,int registros ,String area, String controller, String action, String host)
        {
            pagina_actual = pagina == 0 ? 1 : pagina;
            pagina_cuantos = registros > 0 ? registros : pagina_cuantos;

            int pagina_totalRegistro = table.Count;
            //Procedimiento para calcular las páginas de los registros que se están generando
            double valor1 = Math.Ceiling((double)pagina_totalRegistro / (double)pagina_cuantos);
            int pagina_totalPaginas = Convert.ToInt16(Math.Ceiling(valor1));
            if(pagina_actual !=1)
            {
                //si no está en la página N° 1, ponemos  el enlace "Primera"
                int pagina_url = 1;
                pagina_navegacion += "<a class='btn btn-default' href='" 
                    + host + "/" + controller + "/" + action + "?id=" + pagina_url + "&registros=" + pagina_cuantos + "&area=" + area
                    + "'>" + pagina_nav_primera + "</a>" ;

                //Si no está en la página 1 Ponemos el enlace "Anterior"
                pagina_url = pagina_actual - 1; //Este será el # de página al que enlazamos
                pagina_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagina_url + "&registros=" + pagina_cuantos + "&area=" + area + "'>" + pagina_nav_anterior; 
            }
            /*Si se definió la variable pagina_nav_num_enlaces. Calculamos el intervalo para restar y sumar a partir de la página actual */
            double valor2 = (pagina_nav_num_enlaces/2); 
            int pagina_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            int pagina_nav_desde = pagina_actual - pagina_nav_intervalo; // Calcula desde aquí qué número de página se mostrará
            int pagina_nav_hasta = pagina_actual + pagina_nav_intervalo; //Calcula hasta qué número de página se mostrará
            if(pagina_nav_desde <1 )
            {
                pagina_nav_hasta -= (pagina_nav_desde - 1); //Le sumamos la cantidad sobrante al final para mantener el número de enlaces que se quiere mostrar
                pagina_nav_desde = 1; //Establece pagina_nav_desde como 1
            }
            if(pagina_nav_hasta > pagina_totalPaginas) //Si pagina_nav_hasta es un número mayor que el total de páginas
            {
                pagina_nav_desde -= (pagina_nav_hasta - pagina_totalPaginas); // Le restamos la cantidad excedida al comienzo para antener el número de enlaces que se quiere mostrar
                pagina_nav_hasta = pagina_totalPaginas; // Establecemos pagina_nav_hasta como el total de páginas.
                if(pagina_nav_desde <1)
                {
                    pagina_nav_desde = 1;
                }
            }
            for(int pagina_i = pagina_nav_desde; pagina_i <= pagina_nav_hasta; pagina_i++)
            {
                if(pagina_i == pagina_actual)
                {
                    pagina_navegacion += "<span clas=='btn btn-default' disabled='disabled'>" + pagina_i +"</span>";
                }
                else
                {
                    pagina_navegacion += "<a class='btn btn-default' href='" + host +"/" + action +"?id=" + pagina_i + "&registros=" + pagina_cuantos + "&area=" + area + "'>" + pagina_i +  "</a>";
                }
            }
            if(pagina_actual < pagina_totalPaginas)
            {
                int pagina_url = pagina_actual + 1; //Si no estamos en la última página ponemos el enlace "Siguiente"
                pagina_navegacion += "<a class=''btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagina_url + "&registros=" + pagina_cuantos + "$area" + area + "'>" + pagina_nav_siguiente + "</nav>";

                //Si no estamos en la última página, ponemos el enlace "Última"
                pagina_url = pagina_totalPaginas; //será el número de páginas al que enlazamos
                pagina_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action + "?id=" + pagina_url + "&registros" + pagina_cuantos + "&area=" + area + "'>" + pagina_nav_ultima + "</a>";
            }
            //Obtención de los registros que se mostrarán en la página actual
            int pagina_inicial = (pagina_actual - 1) * pagina_cuantos;
            var query = table.Skip(pagina_inicial).Take(pagina_cuantos).ToList(); //Consulta SQL que devuelve cantidad de registros empezando desde pagina_inicial
            String pagina_informacion = "del <br>" + pagina_actual + "</br> al <br>" + pagina_totalPaginas + "</br> de <br>" + pagina_totalRegistro + "</br> <br>" + pagina_cuantos + "</br>";
            object[] data = { pagina_informacion, pagina_navegacion, query };
            return data;
        }
    }
}
