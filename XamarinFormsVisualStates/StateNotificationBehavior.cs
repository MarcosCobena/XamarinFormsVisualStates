using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinFormsVisualStates
{
    public class StateNotificationBehavior<T> : Behavior<T> where T : Page, IStateNotified
    {
        private const string StatePropertyName = "State";

        private T _bindable;
        private IStateNotifier _viewModel;

        protected override void OnAttachedTo(T bindable)
        {
            bindable.BindingContextChanged += OnBindableBindingContextChanged;
            _bindable = bindable;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(T bindable)
        {
            bindable.BindingContextChanged -= OnBindableBindingContextChanged;

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= OnBindablePropertyChanged;
            }

            base.OnDetachingFrom(bindable);
        }

        private void OnBindableBindingContextChanged(object sender, EventArgs e)
        {
            if (sender is BindableObject bindable && bindable.BindingContext is IStateNotifier viewModel)
            {
                _viewModel = viewModel;
                _viewModel.PropertyChanged += OnBindablePropertyChanged;
            }
        }

        private void OnBindablePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.State))
            {
                _bindable.UpdateVisualState(_viewModel.State);
            }
        }
    }

    public interface IStateNotified
    {
        void UpdateVisualState(string newState);
    }

    public interface IStateNotifier : INotifyPropertyChanged
    {
        string State { get; }
    }
}