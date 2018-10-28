//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Assets.Script.Frame
//{
    
//    public interface IEvent
//    {
//        int evtId { get; }
//        IEventSender sender { get; }
//        //int evtId;
//        //IEventSender GetSender();
//    }

//    public interface IEventSender
//    {
//        //void SendEvent(IEvent ievt);
//    }

//    public interface IEventReceiver
//    {
//        void ProcessEvent(IEvent ievt);
//    }

//    public class XEvent : IEvent
//    {
//        int _evtId;
//        IEventSender _sender;
//        public XEvent(int evtId, IEventSender sender = null)
//        {
//            this._evtId = evtId;
//            this._sender = sender;
//        }

//        public int evtId { get { return _evtId; } }
        
//        public IEventSender sender { get { return _sender; } }

//        public virtual void Send()
//        {
//            EventSystem.me.SendEvent(this);
//        }
//    }

//    public class EventSystem : Singleton<EventSystem>
//    {
//        Queue<IEventOperation> opers = new Queue<IEventOperation>();
//        Dictionary<int, List<IEventReceiver>> recvs = new Dictionary<int, List<IEventReceiver>>();
//        bool sending = false;

//        interface IEventOperation
//        {
//            void Do();
//        }


//        class EvtOper_RegistReceiver : IEventOperation
//        {
//            int evtid;
//            IEventReceiver recv;
//            public EvtOper_RegistReceiver(int evtid, IEventReceiver recv)
//            {
//                this.evtid = evtid;
//                this.recv = recv;

//            }
//            public void Do()
//            {
//                EventSystem.me.RegistEventReceiver(evtid, recv);
//            }
//        }

//        class EvtOper_UnRegistReceiver : IEventOperation
//        {
//            int evtid;
//            IEventReceiver recv;
//            public EvtOper_UnRegistReceiver(int evtid, IEventReceiver recv)
//            {
//                this.evtid = evtid;
//                this.recv = recv;

//            }
//            public void Do()
//            {
//                EventSystem.me.UnrigistEventReceiver(evtid, recv);
//            }
//        }

//        class EvtOper_SendEvent : IEventOperation
//        {
//            IEvent ievt;
//            public EvtOper_SendEvent(IEvent ievt)
//            {
//                this.ievt = ievt;
//            }
//            public void Do()
//            {
//                EventSystem.me.SendEvent(ievt);
//            }

//        }

//        public void RegistEventReceiver(int evtid, IEventReceiver recv)
//        {
//            if (!sending)
//            {
//                RealRegistReceiver(evtid, recv);
//            }
//            else 
//            {
//                opers.Enqueue(new EvtOper_RegistReceiver(evtid, recv));

//            }

//        }

        

//        void RealRegistReceiver(int evtid, IEventReceiver recv)
//        {
//            if(!recvs.ContainsKey(evtid))
//            {
//                recvs[evtid] = new List<IEventReceiver>();
//            }
//            recvs[evtid].Add(recv);
//        }



//        public void UnrigistEventReceiver(int evtid, IEventReceiver recv)
//        {
//            if (!sending)
//            {
//                RealUnrigistEventReceiver(evtid, recv);
//            }
//            else
//            {
//                opers.Enqueue(new EvtOper_UnRegistReceiver(evtid, recv));

//            }
//        }

//        void RealUnrigistEventReceiver(int evtid, IEventReceiver recv)
//        {
//            if (!recvs.ContainsKey(evtid))
//            {
//                // Error;
//                return;
//            }
//            recvs[evtid].Remove(recv);
//        }

//        public void SendEvent(IEvent ievt)
//        {
//            RealSendEvent(ievt);
//            return;
//            if (!sending)
//            {
//                RealSendEvent(ievt);
//            }
//            else
//            {
//                opers.Enqueue(new EvtOper_SendEvent(ievt));
//            }
//        }

//        void RealSendEvent(IEvent ievt)
//        {
//            sending = true;
//            if (recvs.ContainsKey(ievt.evtId))
//            {
//                List<IEventReceiver> list = recvs[ievt.evtId];
//                for (int i=0; i<list.Count; i++)
//                {
//                    IEventReceiver r = list[i];
//                    r.ProcessEvent(ievt);
//                }
//            }
//            sending = false;
//        }

//        public void Update()
//        {
//            while(opers.Count != 0)
//            {
//                IEventOperation op = opers.Dequeue();
//                op.Do();
//            }
//        }
//    }
//}

