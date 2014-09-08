using OHM.Commands;
using OHM.Logger;
using OHM.Sys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace OHM.Nodes
{
    public interface INode : INotifyPropertyChanged
    {

        string Key { get; }

        string Name { get; }

        IList<ICommand> Commands { get; }

        bool CanExecuteCommand(string key);

        bool ExecuteCommand(string key, Dictionary<string, object> arguments);

        IList<INode> Children { get; }

        INode Parent { get; }

        bool AddChild(INode node);

        bool RemoveChild(INode node);

        INode GetChild(string key);

    }

    public abstract class NodeAbstract : INode
    {
        private string _key;
        private string _name;
        private ObservableCollection<ICommand> _commands;
        private Dictionary<string, ICommand> _commandsDic;
        private ObservableCollection<INode> _children;
        private Dictionary<string, INode> _childrenDic;
        private INode _parent;
        private ILogger _logger;
        private Dispatcher _dispatcher;
        public event PropertyChangedEventHandler PropertyChanged;

        public NodeAbstract(string key, string name, ILogger logger, INode parent)
        {
            _key = key;
            _name = name;
            _commands = new ObservableCollection<ICommand>();
            _commandsDic = new Dictionary<string, ICommand>();
            _children = new ObservableCollection<INode>();
            _childrenDic = new Dictionary<string, INode>();
            _parent = parent;
            _logger = logger;
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        #region "Public"

        public string Key
        {
            get { return _key; }
        }

        public string Name
        {
            get { return _name; }
            protected set 
            { 
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public IList<ICommand> Commands { get { return new ReadOnlyObservableCollection<ICommand>(_commands); } }

        public bool CanExecuteCommand(string key)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].CanExecute();
            }
            return false;
        }

        public bool ExecuteCommand(string key, Dictionary<string, object> arguments)
        {
            if (_commandsDic.ContainsKey(key))
            {
                return _commandsDic[key].Execute(arguments);
            }
            return false;
        }

        public IList<INode> Children
        {
            get { return _children; }
        }

        public INode Parent
        {
            get { return _parent; }
        }

        public bool AddChild(INode node)
        {
            if (GetChild(node.Key) == null)
            {
                
                //var d = Dispatcher.CurrentDispatcher;
                _childrenDic.Add(node.Key, node);
                _dispatcher.Invoke((Action)(() =>
                {
                    _children.Add(node);
                }));
                
                return true;
            }
            return false;
        }

        public bool RemoveChild(INode node)
        {
            var it = GetChild(node.Key);
            if (it != null)
            {
                _children.Remove(it);
                _childrenDic.Remove(node.Key);
                return true;
            }
            return false;
        }

        public INode GetChild(string key)
        {
            INode result;

            if(_childrenDic.TryGetValue(key, out result)) {
                return result;
            }
            return null;
    }

        #endregion

        #region "Protected"

        protected bool RegisterCommand(ICommand command)
        {
            if (_commandsDic.ContainsKey(command.Definition.Key))
            {
                return false;
            }
            _commandsDic.Add(command.Definition.Key, command);
            _commands.Add(command);
            return true;
        }

        protected ILogger Logger { get { return _logger; } }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        protected bool Init()
        {
            return true;
        }
    }
}
