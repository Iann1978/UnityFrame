using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Frame
{


    public interface IEventSender
    {
        //void SendEvent(IEvent ievt);
    }

    public interface IEventReceiver
    {
        void ProcessEvent(IEvent ievt);
    }


    public interface IEvent
    {
        int evtId { get; }
        IEventSender sender { get; }
        //int evtId;
        //IEventSender GetSender();
    }

    public class XEvent : IEvent
    {
        int _evtId;
        IEventSender _sender;
        public XEvent(int evtId, IEventSender sender = null)
        {
            this._evtId = evtId;
            this._sender = sender;
        }

        public int evtId { get { return _evtId; } }

        public IEventSender sender { get { return _sender; } }

        public virtual void Send()
        {
            EventSystem.me.postEvent(this);
        }
    }


    class PostEvent : Operation
    {
        IEvent evt;
        public PostEvent(IEvent evt)
        {
            this.evt = evt;
        }
        public void Do()
        {
            EventSystem.me.RealSendEvent(evt);
        }
    }
    interface Operation
    {
        void Do();
    }

    class RegistReceiver : Operation
    {
        int evtId;
        IEventReceiver receiver;
        public RegistReceiver(int evtId, IEventReceiver receiver)
        {
            this.evtId = evtId;
            this.receiver = receiver;
        }
        public void Do()
        {
            EventSystem.me.RealRegistReceiver(evtId, receiver);
        }        
    }

    class UnrgistReceiver : Operation
    {
        int evtId;
        IEventReceiver receiver;
        public UnrgistReceiver(int evtId, IEventReceiver receiver)
        {
            this.evtId = evtId;
            this.receiver = receiver;
        }
        public void Do()
        {
            EventSystem.me.RealUnregistEventReceiver(evtId, receiver);
        }     
    }

    public class EventSystem : Singleton<EventSystem>
    {
        Queue<Operation> operQueue = new Queue<Operation>();
        public Queue<IEvent> evtQueue = new Queue<IEvent>();
        //public HashSet<IEventReceiver> receivers = new HashSet<IEventReceiver>();
        public Dictionary<int, List<IEventReceiver>> receivers = new Dictionary<int, List<IEventReceiver>>();

        public void Reset()
        {
            operQueue = new Queue<Operation>();
            evtQueue = new Queue<IEvent>();
            receivers = new Dictionary<int, List<IEventReceiver>>();
        }
        public void RegistReceiver(int evtid, IEventReceiver receiver)
        {
            operQueue.Enqueue(new RegistReceiver(evtid, receiver));
            //addedReceivers.Add(receiver);
        }
        public void UnRegistReceiver(int evtid, IEventReceiver receiver)
        {
            if (receiver != null)
                operQueue.Enqueue(new UnrgistReceiver(evtid, receiver));
        }
        public void RealRegistReceiver(int evtid, IEventReceiver recv)
        {
            if (!receivers.ContainsKey(evtid))
            {
                receivers[evtid] = new List<IEventReceiver>();
            }
            receivers[evtid].Add(recv);
        }



        public void RealUnregistEventReceiver(int evtid, IEventReceiver recv)
        {
            if (!receivers.ContainsKey(evtid))
            {
                // Error;
                return;
            }
            receivers[evtid].Remove(recv);
        }
        //public void fireEvent(IEvent evt)
        //{
        //    foreach (IEventReceiver iver in receivers)
        //    {
        //        if (iver != null)
        //            iver.ProcessEvent(evt);
        //    }
        //}
        public void RealSendEvent(IEvent ievt)
        {
            if (receivers.ContainsKey(ievt.evtId))
            {
                List<IEventReceiver> list = receivers[ievt.evtId];
                for (int i = 0; i < list.Count; i++)
                {
                    IEventReceiver r = list[i];
                    r.ProcessEvent(ievt);
                }
            }
        }

        public void Update()
        {
            while(operQueue.Count != 0)
            {
                Operation op = operQueue.Dequeue();
                op.Do();
            }
        }


        public void postEvent(IEvent evt)
        {
            operQueue.Enqueue(new PostEvent(evt));
        }

        public void sendEvent(IEvent evt)
        {
            PostEvent postEvent = new PostEvent(evt);
            postEvent.Do();
        }

    }

}
