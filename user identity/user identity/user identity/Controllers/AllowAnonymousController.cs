using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace user_identity.Controllers
{
    //کنترلرهایی که از این کنترلر ارث بری کنند نیاز به مجوز احراز هویت جهت دسترسی نخواهند داشت.
    //اگر نیاز به دسترسی فقط به یک اکشن از کنترلر بود و بقیه اکشن ها میبایست احراز هویت گردند از این متد ارث بری نشود سپس بالای اکشن بی نیاز از احراز هویت 
    //از AllowAnonymous استفاده شود
    [AllowAnonymous]
    public class AllowAnonymousController : BaseController
    {
    }
}