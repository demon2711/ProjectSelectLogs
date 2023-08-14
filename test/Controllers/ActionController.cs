using Microsoft.AspNetCore.Mvc;
using test.Data;
using test.Models;
using Korzh.EasyQuery.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;
using System.Linq;

namespace test.Controllers
{
    [Authorize]
    public class ActionController : Controller
    {
        public ApplicationDbContext _context;
        public static int number = 0;
        public static int numberAction = 0;
        
        public static string[] strings;
        public static string[] stringsAction;

        public static List<Models.Action> tempActions;


        public ActionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        
        public IActionResult Index(String? searchString, String? selectLabel, String? startNumber, String? endNumber, String? actionType, String? check = "off", int page = 1)
        {

            var actions = from m in _context.AuditTrail
                          select m;

            if (check == "off")
            {
                actionType = null;
            }
            bool a = false;
            bool first = false;
            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(startNumber) && String.IsNullOrEmpty(endNumber))
            {
                ViewData["Information"] = "Введите необходимые параметры для поиска.";
                first = true;
                          
            }
            

            ViewData["Search"] = searchString;
            ViewData["StartNumber"] = startNumber;
            ViewData["EndNumber"] = endNumber;
            number++;
            if (String.IsNullOrEmpty(searchString))
            {
                strings = new string[6];
                strings[0] = "SearchAll";
                strings[1] = "UserLogin";
                strings[2] = "Application";
                strings[3] = "ActionType";
                strings[4] = "ActionObjectID";
                strings[5] = "ExtraInformation";
            }

            string[] stringSort = new string[6];
            stringSort[0] = "SearchAll";
            stringSort[1] = "UserLogin";
            stringSort[2] = "Application";
            stringSort[3] = "ActionType";
            stringSort[4] = "ActionObjectID";
            stringSort[5] = "ExtraInformation";

            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(startNumber) && String.IsNullOrEmpty(endNumber))
            {
                ViewData["checkBoxClicked"] = "off";
                stringsAction = new string[29];              
                stringsAction[0] = "User_Login";
                stringsAction[1] = "User_Logout";
                stringsAction[2] = "Create_Title";
                stringsAction[3] = "Modify_Title_Status";
                stringsAction[4] = "Delete_Title";
                stringsAction[5] = "Restore_Title";
                stringsAction[6] = "Purge_Item";
                stringsAction[7] = "Move_Title";
                stringsAction[8] = "Create_Title_Link";
                stringsAction[9] = "Delete_Title_Link";
                stringsAction[10] = "Modify_Title_Metadata";
                stringsAction[11] = "Modify_Title_Media";
                stringsAction[12] = "Purge_Title_Media";
                stringsAction[13] = "Create_Category";
                stringsAction[14] = "Delete_Category";
                stringsAction[15] = "Restore_Category";
                stringsAction[16] = "Purge_Category";
                stringsAction[17] = "Modify_Category";
                stringsAction[18] = "Empty_Recycle_Bin";
                stringsAction[19] = "Search_Query";
                stringsAction[20] = "Restricted_Media_Used";
                stringsAction[21] = "Metadata_Import";
                stringsAction[22] = "Metadata_Import_Error";
                stringsAction[23] = "Delete_Clock";
                stringsAction[24] = "Download_Title";
                stringsAction[25] = "Rundown_Insert_Items";
                stringsAction[26] = "Rundown_Move_Items";
                stringsAction[27] = "Rundown_Update_Items";
                stringsAction[28] = "Rundown_Remove_Items";
            }

            string[] stringSortAction = new string[29];           
            stringSortAction[0] = "User_Login";
            stringSortAction[1] = "User_Logout";
            stringSortAction[2] = "Create_Title";
            stringSortAction[3] = "Modify_Title_Status";
            stringSortAction[4] = "Delete_Title";
            stringSortAction[5] = "Restore_Title";
            stringSortAction[6] = "Purge_Item";
            stringSortAction[7] = "Move_Title";
            stringSortAction[8] = "Create_Title_Link";
            stringSortAction[9] = "Delete_Title_Link";
            stringSortAction[10] = "Modify_Title_Metadata";
            stringSortAction[11] = "Modify_Title_Media";
            stringSortAction[12] = "Purge_Title_Media";
            stringSortAction[13] = "Create_Category";
            stringSortAction[14] = "Delete_Category";
            stringSortAction[15] = "Restore_Category";
            stringSortAction[16] = "Purge_Category";
            stringSortAction[17] = "Modify_Category";
            stringSortAction[18] = "Empty_Recycle_Bin";
            stringSortAction[19] = "Search_Query";
            stringSortAction[20] = "Restricted_Media_Used";
            stringSortAction[21] = "Metadata_Import";
            stringSortAction[22] = "Metadata_Import_Error";
            stringSortAction[23] = "Delete_Clock";
            stringSortAction[24] = "Download_Title";
            stringSortAction[25] = "Rundown_Insert_Items";
            stringSortAction[26] = "Rundown_Move_Items";
            stringSortAction[27] = "Rundown_Update_Items";
            stringSortAction[28] = "Rundown_Remove_Items";
           // if (page == 1)
           // {
                

                if (!String.IsNullOrEmpty(selectLabel))
                {

                    //------------------------------------------------Поиск по дате и времени---------------------------------------------------------------------

                    if (String.IsNullOrEmpty(searchString))
                    {
                        if (check != null)
                        {
                            if (check.Equals("off"))
                            {
                                actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber));
                                changeExtraInfo(actions);

                                ViewData["checkBoxClicked"] = "off";
                            }
                            else if (check.Equals("on"))
                            {
                                for (int i = 0; i < stringSortAction.Length; i++)
                                {
                                    if (actionType.Equals(stringSortAction[i]))
                                    {
                                        actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ActionType!.Equals(actionType));
                                        changeExtraInfo(actions);
                                        ViewData["checkBoxClicked"] = "on";
                                        break;
                                    }
                                }
                            }
                        }
                    }


                    //-------------------------------------------Поиск по дате и времени + по запросу-----------------------------------------------------------


                    else if (!String.IsNullOrEmpty(selectLabel) && !String.IsNullOrEmpty(searchString))
                    {
                        if (check != null) {
                            if (check.Equals("off"))
                            {
                                ViewData["checkBoxClicked"] = "off";

                                ViewData["Error"] = "";

                                if (selectLabel.Equals("SearchAll"))
                                {
                                    actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && (s.UserLogin!.Contains(searchString) ||
                                    s.Application!.Contains(searchString) ||
                                    s.ActionType!.Contains(searchString) ||
                                    s.ActionObjectID!.Contains(searchString) ||
                                    s.ExtraInformation!.Contains(searchString)));

                                }

                                else if (selectLabel.Equals("UserLogin"))
                                {
                                    actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.UserLogin!.Equals(searchString));

                                }

                                else if (selectLabel.Equals("Application"))
                                {
                                    actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.Application!.Equals(searchString));
                                }

                                else if (selectLabel.Equals("ActionObjectID"))
                                {
                                    actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ActionObjectID!.Equals(searchString));
                                }

                                else if (selectLabel.Equals("ActionType"))
                                {
                                    actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ActionType!.Equals(searchString));
                                }

                                else if (selectLabel.Equals("ExtraInformation"))
                                {
                                    actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ExtraInformation!.Contains(searchString));
                                }
                                changeExtraInfo(actions);
                            }
                            else if (check == "on")
                            {
                                ViewData["checkBoxClicked"] = "on";
                                if (selectLabel.Equals("SearchAll"))
                                {
                                    for (int i = 0; i < stringSortAction.Length; i++)
                                    {
                                        if (actionType.Equals(stringSortAction[i]))
                                        {
                                            actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ActionType!.Equals(actionType) && (s.UserLogin!.Equals(searchString) ||
                                            s.Application!.Contains(searchString) ||
                                            s.ActionObjectID!.Contains(searchString) ||
                                            s.ExtraInformation!.Contains(searchString)));
                                            break;
                                        }
                                    }
                                }

                                else if (selectLabel.Equals("UserLogin"))
                                {
                                    for (int i = 0; i < stringSortAction.Length; i++)
                                    {
                                        if (actionType.Equals(stringSortAction[i]))
                                        {
                                            actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.UserLogin!.Equals(searchString) && s.ActionType!.Equals(actionType));
                                            break;
                                        }
                                    }
                                }

                                else if (selectLabel.Equals("Application"))
                                {
                                    for (int i = 0; i < stringSortAction.Length; i++)
                                    {
                                        if (actionType.Equals(stringSortAction[i]))
                                        {
                                            actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.Application!.Equals(searchString) && s.ActionType!.Equals(actionType));
                                            break;
                                        }
                                    }
                                }

                                else if (selectLabel.Equals("ActionObjectID"))
                                {

                                    for (int i = 0; i < stringSortAction.Length; i++)
                                    {
                                        if (actionType.Equals(stringSortAction[i]))
                                        {
                                            actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ActionType!.Equals(actionType) && s.ActionObjectID!.Equals(searchString));
                                            break;
                                        }
                                    }
                                }

                                else if (selectLabel.Equals("ExtraInformation"))
                                {
                                    for (int i = 0; i < stringSortAction.Length; i++)
                                    {
                                        if (actionType.Equals(stringSortAction[i]))
                                        {
                                            actions = actions.Where(s => s.Date_Time >= Convert.ToDateTime(startNumber) && s.Date_Time <= Convert.ToDateTime(endNumber) && s.ExtraInformation!.Contains(searchString) && s.ActionType!.Equals(actionType));
                                            break;
                                        }
                                    }
                                }
                                changeExtraInfo(actions);
                            }
                        }
                    }
                }

                


            //}
            //else
            //{
                
             //   tempActions = new List<Models.Action>(actions);
            //}

            //-----------------------------------------Воостановление селекта---------------------------------------------------------------------          

            if (!String.IsNullOrEmpty(selectLabel))
            {
                selectField(selectLabel, strings, stringSort, strings.Length);
                selectField(actionType, stringsAction, stringSortAction, stringsAction.Length);
            }

            viewData("", strings, strings.Length);
            viewData("Action", stringsAction, stringsAction.Length);
            
            IndexViewModel viewModel;

            

            if (!actions.Any())
            {
                PageViewModel pageViewModel = new PageViewModel(1, 1, 1);
                viewModel = new IndexViewModel
                {
                    PageViewModel = pageViewModel,
                    Actions = actions,
                };
            }
            
            else
            {
                int pageSize = 15;                
                int count = 0;
                if (!first)
                {
                    foreach (var action in actions)
                    {
                        count++;
                    }
                }
                var items = actions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                viewModel = new IndexViewModel
                {
                    PageViewModel = pageViewModel,
                    Actions = items,
                };
            }
            ViewData["TotalPages"] = viewModel.PageViewModel.TotalPages;

            
            

            return View(viewModel);            
        }

        
        public IActionResult Authorization(String? login, String? password)
        {
            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password))
            {
                char[] temp = login.ToCharArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = Char.ToLower(temp[i]);
                }
                
                login = new string(temp);

                if (login.Equals("audit") && password.Equals("@ud1t"))
                {
                    //return Redirect("https://yandex.ru/");
                    ViewData["errorLoginOrPassword"] = "";
                    return RedirectToRoute("Action", new { controller = "Action", action = "Index" });
                }
                else
                {
                    ViewData["login"] = login;
                    ViewData["errorLoginOrPassword"] = "error";
                    return View();
                }
            }           
            return View();
        }

        public void selectField(string select, string[] stringsSelect, string[] sortStringSelect, int n)
        {
            if (!string.IsNullOrEmpty(select))
            {
                for (int i = 0; i < n; i++)
                {
                    if (stringsSelect[i].Equals(select))
                    {
                        stringsSelect[i] = stringsSelect[0];
                        stringsSelect[0] = select;
                        break;
                    }

                }
                int j = 0;
                for (int i = 1; i < n; i++)
                {
                    if (select.Equals(sortStringSelect[j]))
                    {
                        i--;
                    }
                    else
                    {
                        stringsSelect[i] = sortStringSelect[j];
                    }
                    j++;
                }
            }
            
        }

        public void viewData(string nameViewData, string[] stringsViewData, int n)
        {
            int number = 0;
            for (int i = 0; i < n; i++)
            {
                number = i + 1;
                ViewData["Select" + nameViewData + number] = stringsViewData[i];
            }
        }
        public void changeExtraInfo(IQueryable<Models.Action> actions)
        {
            List<string> extraInfo = new List<string>();
            StringBuilder tempString = new StringBuilder();
            StringBuilder word = new StringBuilder();
            List<List<string>> finalStrings = new List<List<string>>();
            bool resolution;
            bool range;
            bool close;
            int k = 0;
            int j = 0;
            int counter = 0;

            foreach (var action in actions)
            {
                tempString.Clear();
                tempString.Append(action.ExtraInformation);
                j = 0;
                range = false;
                close = false;
                int countList = 0;

                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] == '&' && tempString[i + 1] == 'l' && tempString[i + 2] == 't' && tempString[i + 3] == ';')
                    {
                        tempString.Remove(i, 4);
                        tempString.Insert(i, "<");
                    }
                    
                    if (tempString[i] == '&' && tempString[i + 1] == 'g' && tempString[i + 2] == 't' && tempString[i + 3] == ';')
                    {
                        tempString.Remove(i, 4);
                        tempString.Insert(i, ">");
                    }
                }

                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] == '<')
                    {
                        close = false;
                        if (tempString[i + 1] == '/')
                            close = true;
                        j = i + 1;
                        while (tempString[j] != '<')
                        {
                            j++;
                            if (j == tempString.Length)
                            {
                                range = true;
                                break;
                            }
                        }
                        if (!range)
                        {
                            if (tempString[j + 1] != '/')
                            {
                                tempString.Remove(i, j - i);
                                i--;
                            }
                            else if (tempString[j + 1] == '/' && close)
                            {
                                tempString.Remove(i, j - i);
                                i--;
                            }
                        }
                        else
                        {
                            tempString.Remove(i, tempString.Length - i);
                        }
                    }
                }

                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] == '>' && i != tempString.Length - 1)
                    {
                        k = i + 1;
                        while (tempString[k] != '<')
                        {
                            if (k + 1 == tempString.Length)
                            {
                                break;
                            }
                            k++;
                        }
                        if (k + 1 != tempString.Length)
                        {
                            tempString.Insert(k, "\r\n", 1);
                            i = i + 2;
                        }
                    }
                }

                resolution = false;
                for (int i = 0; i < tempString.Length; i++)
                {
                    if (i < tempString.Length - 2)
                    {
                        if (tempString[i] == '<' && tempString[i + 1] == 's' && tempString[i + 2] == '>')
                        {
                            tempString.Remove(i + 1, 1);
                            tempString.Insert(i + 1, "Value");
                        }
                    }

                    word.Clear();
                    if (tempString[i] == '<' && i != tempString.Length - 1)
                    {
                        tempString[i + 1] = char.ToUpper(tempString[i + 1]);

                        resolution = true;
                        j = i + 1;
                        while (tempString[j] != '>' && j != tempString.Length - 1)
                        {
                            if (tempString[j] == ' ')
                            {
                                resolution = false;
                            }
                            if (resolution)
                                word.Append(tempString[j]);
                            j++;
                        }
                        word.Append(": ");
                        tempString.Remove(i, j - i + 1);
                        tempString.Insert(i, word.ToString());
                        i = i + word.Length;
                    }
                }

                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] == '&' && tempString[i + 1] == 'q' && tempString[i + 2] == 'u' && tempString[i + 3] == 'o' && tempString[i + 4] == 't' && tempString[i + 5] == ';')
                    {
                        tempString.Remove(i, 6);
                        tempString.Insert(i, "\"");
                    }
                    if (tempString[i] == '&' && tempString[i + 1] == 'a' && tempString[i + 2] == 'm' && tempString[i + 3] == 'p' && tempString[i + 4] == ';')
                    {
                        tempString.Remove(i, 5);
                        tempString.Insert(i, " ");
                    }
                    if (tempString[i] == '&' && tempString[i + 1] == 'a' && tempString[i + 2] == 'p' && tempString[i + 3] == 'o' && tempString[i + 4] == 's' && tempString[i + 5] == ';')
                    {
                        tempString.Remove(i, 6);
                        tempString.Insert(i, "\'");
                    }
                    /*if (counter == 58)
                    {
                        tempString.Insert(i, "\r\n", 1);
                        counter = 0;
                    }
                    if (tempString[i] == '\r' || tempString[i] == '\n')
                    {
                        counter = 0;
                    }
                    else
                        counter++;
                    if (i == tempString.Length-1)
                        counter = 0;*/
                }
                bool error = false;
                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] == '\n' && i < tempString.Length - 3)
                    {
                        int count = i + 1;
                        if (error)
                        {
                            break;
                        }
                        while (tempString[count] != '\n')
                        {
                            if (count == tempString.Length - 3)
                            {
                                error = true;
                                break;
                            }
                            if (tempString[count] == ':' && tempString[count + 1] == ' ' && tempString[count + 2] == '\r')
                            {
                                tempString.Remove(i-1, count + 3 - (i + 1));
                                break;
                            }
                            count++;
                        }
                    }
                }
                List<string> tempList = new List<string>();
                bool cntrPoint = false;
                StringBuilder subStr1 = new StringBuilder();
                StringBuilder subStr2 = new StringBuilder();
                for (int i = 0; i < tempString.Length; i++)
                {
                    if (tempString[i] == '\n')
                    {
                        subStr2.Append(tempString[i]);
                        cntrPoint = false;
                        tempList.Add(subStr1.ToString());
                        tempList.Add(subStr2.ToString());
                        subStr1.Clear();
                        subStr2.Clear();
                    }
                    else
                    {
                        if (!cntrPoint)
                        {
                            subStr1.Append(tempString[i]);
                        }
                        else
                        {
                            subStr2.Append(tempString[i]);
                        }
                        if (tempString[i] == ':')
                        {
                            cntrPoint = true;
                        }
                    }
                }
                finalStrings.Add(tempList);
                extraInfo.Add(tempString.ToString());
            }
            ViewData["ExtraInformationList"] = finalStrings;            
        }
    }
}
