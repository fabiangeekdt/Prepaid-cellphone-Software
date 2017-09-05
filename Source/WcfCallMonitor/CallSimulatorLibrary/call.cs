using System;
using System.Collections.Generic;
using System.Linq;
using CallSimulatorLibrary.Entity;

namespace CallSimulatorLibrary
{
    public class call
    {
        public call()
        {

        }

        public CallSimulatorResponse startcall(CallSimulatorRequest e)
        {
            try
            {
                Phonecall(e);
                var response = new CallSimulatorResponse();
                response = Phonecall(e);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private int timeCounting(DateTime startCall, int minutesLet, int h)
        {
            throw new NotImplementedException();
        }
    }
}
