using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartCounter.View.Behaviors
{
    public class ClearAllTextWhenEnterEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.Focused += EntryFocusedChangedHandler;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Focused -= EntryFocusedChangedHandler;
            base.OnDetachingFrom(entry);
        }

        private void EntryFocusedChangedHandler(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                var entry = sender as Entry;
                entry.Text = "0";
            }
        }
    }
}
