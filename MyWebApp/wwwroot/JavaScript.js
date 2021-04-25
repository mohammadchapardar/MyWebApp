function AddProduct() {
    if (confirm('تغییرات ذخیره شود ؟')) {
        window.location = '@Html.ActionLink("Product","Detail")';
    }
    else {
        window.location = '@Html.ActionLink("Product","AddOrEdit")';
    }
}

function RemoveProduct() {
    if (confirm('آیا به حذف محصول اطمینان دارید؟')) {
        window.location = '@(Url.Action("Delete", "Product",new {Id=Model.Id }))';
    }
}
function Test() {
    alert("Hello");
}