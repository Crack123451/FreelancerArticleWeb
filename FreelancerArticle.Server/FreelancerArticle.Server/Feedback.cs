//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreelancerArticle.Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class Feedback
    {
        public int C__Отклика { get; set; }
        public string Фрилансер { get; set; }
        public Nullable<int> C__Заказа { get; set; }
    
        public virtual Freelancer Freelancer { get; set; }
        public virtual Order Order { get; set; }
    }
}
