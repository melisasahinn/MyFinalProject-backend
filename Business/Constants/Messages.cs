using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Constants :projedeki sabitler yazılır
namespace Business.Constants
{
    public static class Messages
    {
        public static string MaintenanceTime="Sistem bakımda";
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductListed ="Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string CheckIfProductNameAlreadyExists ="Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded ="Kategori Limiti Aşıldığı için yeni ürün eklenemiyor.";
        public static string AuthorizationDenied ="Yetkiniz yok";
        public static string UserRegistered="mmm";
        public static string UserNotFound="jhjh";
        public static string PasswordError="jkk";
        public static string SuccessfulLogin="hhjh";
        public static string UserAlreadyExists="hjhj";
        public static string AccessTokenCreated="jhjh";
    }
}
