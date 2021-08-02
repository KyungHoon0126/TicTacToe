using System;
using System.Windows.Input;

namespace TicTacToe.Local.Mvvm
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields
        /// <summary>
        /// Action<T> : 하나의 파라미터를 받아들이고, 리턴 값이 없는 함수에 사용되는 Delegate.
        /// </summary>
        readonly Action<T> _execute = null;
        /// <summary>
        /// Predicate<T> : Action/Func delegate와 비슷한데, 리턴값이 반드시 bool이고 입력값이 T 타입인 delegate이다
        /// </summary>
        readonly Predicate<T> _canExecute = null;
        #endregion

        #region Constructors
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        /// <summary>
        /// CanExecuteChanged 이벤트 : ICommand에 바인딩 된 모든 명령 소스(ex: Button or MenuItem)에 CanExecute에 의해 반환 된 값이 변경 되었음을 알린다.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            /// <summary>
            /// CommandManager.RequerySuggested 이벤트
            /// : CommandManager가 명령 실행에 영향을 줄 수 있는 변경 사항이 있다고 생각할 때마다 발생하며 이때마다 CanExecute가 호출된다.
            /// : CanExecute 메서드를 강제로 실행할 수 있다.
            /// : CommandManager의 RequerySuggested에 위임되어 모든 종류의 UI 상호작용을 통해 변경사항이 호출되는 정확한 알림을 제공한다.
            /// </summary>
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 실제 처리해야 하는 작업 기술.
        /// CommandManager.RequerySuggested 이벤트가 호출될 때 마다 실행. 즉 CanExecuteChanged 이벤트가 호출될 때마다 실행.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        /// <summary>
        /// Execute 메서드의 코드를 실행할지 여부를 결정하는 코드 기술.
        /// UI 상호 작용 중에 대부분 발생.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter) => _execute((T)parameter);
        #endregion
    }
}
