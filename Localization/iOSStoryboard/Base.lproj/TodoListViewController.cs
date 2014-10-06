// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace LionTodo
{
	public partial class TodoListViewController : UITableViewController
	{
		public TodoListViewController (IntPtr handle) : base (handle)
		{
			//Title = NSBundle.MainBundle.LocalizedString ("LionTodo", "LionTodo");
		}

		public TodoListViewController ()
		{
			//Title = NSBundle.MainBundle.LocalizedString ("LionTodo", "LionTodo");
		}

		UIBarButtonItem addButton;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, (s,e) =>{
				var todo = new Todo() {
					Title= Lion.Localize("<new task>", "<new task>") 
				};

				AppDelegate.Current.TodoDB.Save (todo);

				var detail = Storyboard.InstantiateViewController("detail") as TodoViewController;
				detail.SetTask (this, todo);
				NavigationController.PushViewController (detail, true);
			});
			NavigationItem.RightBarButtonItem = addButton;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			TableView.Source = new TodoTableSource (AppDelegate.Current.TodoDB.GetAll ());
		}

		/// <summary>
		/// Prepares for segue.
		/// </summary>
		/// <remarks>
		/// The prepareForSegue method is invoked whenever a segue is about to take place. 
		/// The new view controller has been loaded from the storyboard at this point but 
		/// it’s not visible yet, and we can use this opportunity to send data to it.
		/// </remarks>
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "todosegue") { // set in Storyboard
				var navctlr = segue.DestinationViewController as TodoViewController;
				if (navctlr != null) {
					var source = TableView.Source as TodoTableSource;
					var rowPath = TableView.IndexPathForSelectedRow;
					var item = source.GetItem(rowPath.Row);
					navctlr.SetTask(this, item);
				}
			}
		}
	}
}