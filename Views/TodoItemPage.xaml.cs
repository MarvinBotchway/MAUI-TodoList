using TodoList.Data;
using TodoList.Models;

namespace TodoList.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TodoItemPage : ContentPage
{
    public TodoItemPage()
    {
        InitializeComponent();
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItemModel)BindingContext;
        TodoItemDb database = await TodoItemDb.Instance;
        await database.SaveItemAsync(todoItem);
        await Navigation.PopAsync();
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        var todoItem = (TodoItemModel)BindingContext;
        TodoItemDb database = await TodoItemDb.Instance;
        await database.DeleteItemAsync(todoItem);
        await Navigation.PopAsync();
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}