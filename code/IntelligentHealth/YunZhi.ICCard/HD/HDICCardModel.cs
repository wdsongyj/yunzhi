using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YunZhi.Core.ICCard;

namespace YunZhi.ICCard
{
    public class HDICCardModel : IICCardModel
    {
        public HDICCardModel()
        {

        }
        public HDICCardModel(bool state)
        {
            this.State = state;
        }

        public string Name {  get;set; }
        public string Sex {  get;set; }
        public string SexCode {  get;set; }
        public string NationCode {  get;set; }
        public string Nation {  get;set; }
        public string ICCard {  get;set; }
        public int Age {  get;set; }
        public DateTime Birthday {  get;set; }
        public DateTime StartEffectiveDate {  get;set; }
        public DateTime EndEffectiveDate {  get;set; }
        public string Address {  get;set; }
        public string Issuer {  get;set; }
        public string Picture {  get;set; }
        public bool State {  get;set; }
        public string ErrorMessage {  get;set; }
    }
}
