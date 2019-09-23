using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_LamViecVoiEE.Models
{
    public class DataProvider
    {
        /// <summary>
        /// Khai báo 1 thuộc tính để làm việc với db trong EF
        /// </summary>
        private static eNewsEntities1 _Entities = new eNewsEntities1();
        public static eNewsEntities1 Entities
        {
            get { return _Entities; }
        }
    }
}