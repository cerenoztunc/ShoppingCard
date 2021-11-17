using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSepet_2.CustomTools
{
    public class Cart
    {

        Dictionary<int, CartItem> _sepetUrunlerim;

        public Cart()
        {
            _sepetUrunlerim = new Dictionary<int, CartItem>();
        }

        public List<CartItem> Sepetim
        {
            get
            {
                return _sepetUrunlerim.Values.ToList();
            }
        }

        public void SepeteEkle(CartItem item)
        {
            //Öncelikle Sepete daha önce o aynı ürün atılmıs mı onu sorguluyoruz...
            if (_sepetUrunlerim.ContainsKey(item.ID))
            {
                //Bir Dictionary'e index parantezi verirseniz o key'i yakalamak istediginiz bildirir...
                _sepetUrunlerim[item.ID].Amount += 1;
                return;
            }
            _sepetUrunlerim.Add(item.ID, item);
        }

        public void SepettenSil(int id)
        {
            if (_sepetUrunlerim[id].Amount>1)
            {
                _sepetUrunlerim[id].Amount -= 1;
                return;
            }
            _sepetUrunlerim.Remove(id);
        }


        //Sepetin Toplam Fiyatı

        public decimal? TotalPrice
        {
            get
            {
                return _sepetUrunlerim.Sum(x => x.Value.SubTotal);
            }
        }
    }
}