#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 2, 2017
* Description:	
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using BusinessTier.Operations;
using Common.Entities;
using Common.Enum;
using Common.Helpers;
using Common.Validations;
using DataTier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier.Transactions
{
    public class PhoneCall
    {
        DAO dataAccess;
        Response resp;
        Call call;
        Customer cus;
        CustomerPhone cusPhone;
        Recharge rec;
        BusinessValidations transValidations;
        CustomerBonus cusBonus;

        public PhoneCall()
        {
            dataAccess = new DAO();
            cus = new Customer();
            resp = new Response();
            cusPhone = new CustomerPhone();
            rec = new Recharge();
            call = new Call();
            cusBonus = new CustomerBonus();
            transValidations = new BusinessValidations();
        }

        public string startPhoneCall(Stream data)
        {
            try
            {
                //primero valido si el cliente existe
                    //segundo valido si el cliente tiene saldo
                        //si el cliente tiene saldo, se hacen las validaciones de los bonuses
                                //se hace la llamada
                                // se crea una variable de sesion de equipo para que guarde el tiempo de llamada:se cuenta el tiempo de inicio de llamada - inicio de llamada + el tiempo disponible
                                    // si el saldo se termina se cuelga la llamada
                                        //se guarda el saldo cero en la base de datos 
                                        //se envia mensaje indicando que se acabo el saldo
                                    // si el cliente cuelga
                                        //se calcula el saldo sobrante
                                        //se guarda el estado, costo y duracion de la llamada.
                    //si no tiene saldo se envia mensaje que la llamada no puede hacerse
                //Si no existe si termina la llamada
                //fin 
                call = SerializationHelpers.DeserializeJson<Call>(data);

                cus = dataAccess.getCustomerPerPhone(call.Id_Type, call.Id, call.PhoneNumber);
                transValidations.validateCustomerSubscription(cus);

                cusPhone = dataAccess.getBalance(cus.Id, cus.PhoneNumber);
                transValidations.validateMinLeft(cusPhone.MinuteBalance);

                makePhoneCall();
                return string.Empty;
            }
            catch (Exception ex)
            {
                //guardar el estado de la llamada.3   UnReachable
                resp.idResponse = 444;
                resp.response = ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : "");
                resp.exception = ex;
                return SerializationHelpers.SerializeJson<Response>(resp);
            }
        }

        private void makePhoneCall()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public string endPhoneCall(Stream data)
        {
            throw new NotImplementedException();
        }
    }
}
