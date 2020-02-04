using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace api.Services
{
    public class MenuService : IMenuService
    {
        public CommonSearchView<MasterMenuView> Search(MasterMenuSearchView model)
        {
            using (var ctx = new ConXContext())
            {
                //define model view
                CommonSearchView<MasterMenuView> view = new ModelViews.CommonSearchView<ModelViews.MasterMenuView>()
                {
                    pageIndex = model.pageIndex - 1,
                    itemPerPage = model.itemPerPage,
                    totalItem = 0,

                    datas = new List<ModelViews.MasterMenuView>()
                };

                //query data
                List<su_menu> menu = ctx.menu
                    .Where(x => (x.MENU_ID.Contains(model.menuFunctionId) || model.menuFunctionId == "")
                    && (x.MENU_NAME.Contains(model.menuFunctionName) || model.menuFunctionName == "")
                    && (x.MENU_ID.Contains("MOB"))
                    )
                    .OrderBy(o => o.MENU_ID)
                    .ToList();

                //count , select data from pageIndex, itemPerPage
                view.totalItem = menu.Count;
                menu = menu.Skip(view.pageIndex * view.itemPerPage)
                    .Take(view.itemPerPage)
                    .ToList();

                //prepare model to modelView
                foreach (var i in menu)
                {
                    view.datas.Add(new ModelViews.MasterMenuView()
                    {
                        menuFunctionId = i.MENU_ID,
                        menuFunctionGroupId = i.MAIN_MENU,
                        menuFunctionName = i.MENU_NAME,
                        iconName = i.ICON_NAME,
                        menuURL = i.LINK_NAME,
                        //orderDisplay = i.orderDisplay

                    });
                }

                //return data to contoller
                return view;
            }
        }


        public void Update(MasterMenuView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    su_menu updateObj = ctx.menu.Where(z => z.MENU_ID == model.menuFunctionId).SingleOrDefault();

                    updateObj.MENU_ID = model.menuFunctionId;
                    updateObj.MAIN_MENU = model.menuFunctionGroupId;
                    updateObj.MENU_NAME = model.menuFunctionName;
                    updateObj.LINK_NAME = model.menuURL;
                    updateObj.ICON_NAME = model.iconName;

                    ctx.Configuration.AutoDetectChangesEnabled = true;
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }


        public MasterMenuView GetInfo(string code)
        {
            using (var ctx = new ConXContext())
            {
                su_menu model = ctx.menu
                    .Where(z => z.MENU_ID == code).SingleOrDefault();

                return new MasterMenuView
                {
                    menuFunctionId = model.MENU_ID,
                    menuFunctionGroupId = model.MAIN_MENU,
                    menuFunctionName = model.MENU_NAME,
                    menuURL = model.LINK_NAME,
                    iconName = model.ICON_NAME
                    
                };
            }
        }

        
    }
}