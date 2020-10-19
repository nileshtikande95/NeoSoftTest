using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSoftTest.Models
{
    public class SqlHelper
    {
        NeoSoftDBEntities db = new NeoSoftDBEntities();


        public List<tblNeoData> GetList()
        {
            return db.fn_GetData().ToList();
        }

        public void InssertData(tblNeoData detail)
        {

            db.fn_NeoCreate(detail.EmailAddress, detail.CountryId, detail.StateId,
                  detail.CityId, detail.PanNumebr, detail.PassportNumber, detail.ProfileImage, detail.Gender, detail.IsActive);

            db.SaveChanges();

        }


    }
}