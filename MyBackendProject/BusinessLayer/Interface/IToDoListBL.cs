using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IToDoListBL
    {
        public string AddItem(ToDoListModel add);
        public List<ToDoListModel> GetList();
        public UpdateModel UpdateTask(UpdateModel update);
        public bool DeleteTask(int ToDoListId);
    }
}
