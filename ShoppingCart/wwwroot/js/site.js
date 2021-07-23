const productUri = 'api/products';
const catUri = 'api/categories';
const salesUri = 'api/sales';
let cart = [];
let saveMe = [];

$(document).ready(function () {
  //default drop down to no category
  _loadDDL();

  //initilize cart count
  _displayCount();

  //Load Items based on Category
  $('#ddlCategory').change(function () {
    fetch(`${productUri}?category=${this.value}`)
      .then(response => response.json())
      .then(data => _displayItems(data))
      .catch(error => console.error('Unable to get items.', error));
  });

  $('#btnCheckout').click(function () {

    _displayCart()
    $('#modCheckout').modal('show');
  });

  $('#btnPlaceOrder').click(function () {
    $('#modCheckout').modal('hide');
    $('#modContact').modal('show');
  });

  $('#btnComplete').click(function () {
    var name = $('#fullname').val();
    var emailAdd = $('#address').val();
    var txt = "<h4>Thank you for your order " + name + "! Information will be sent to " + emailAdd + "</h4>";
     cart.forEach(item => {
       var sale = { id: item.id, name: item.name, price: item.price, quantity: item.quantity, fullname: name, email: emailAdd };
       const request = new Request(salesUri, {
         method: 'POST',
         body: JSON.stringify(sale),
         headers: new Headers({'Content-Type': 'application/json' })
       });
   fetch(request)
         .then(res => res.json())
         .then(res => console.log(res));
     });
    
    $("#finalMessage").html(txt);
    $('#modContact').modal('hide');
    $('#modFinal').modal('show');
  });

  $('#btnFinal').click(function () {
    cart = [];
    $('select option[value="1"]').attr("selected", true);

    $('#modFinal').modal('hide');
    $('#fullname').val("");
    $('#address').val("");
    _displayCount();

  });




});

function _loadDDL() {
  fetch(catUri)
    .then(response => response.json())
    .then(data => _addDllItem(data))
    .catch(err => console.error(err));
}

function _addDllItem(data) {

  data.forEach(c => $("#ddlCategory").append($("<option></option>").val(c.category).html(c.category)));

}
function addItemToCart(id) {
  fetch(`${productUri}/${id}`)
    .then(response => response.json())
    .then(data => _addToCart(data))
    .catch(error => console.error('Unable to get item.', error));
}

function _addToCart(data) {
  var addNew = true;
  cart.forEach(v => {
    //Test if projectname  == parameter1. If it is update status
    if (v.id === data.id) {
      v.quantity = v.quantity + 1;
      addNew = false;
    }
  });

  if (addNew) cart[cart.length] = { id: data.id, name: data.name, price: data.price, quantity: 1 };
  _displayCount();

}

function removeItemFromCart(id) {

  let workingCart = cart;
  cart = [];

  workingCart.forEach(item => {
    if (item.id === id) {
      //donothing
    }
    else {
      cart.push(item);
    }

  });
  _displayCount();
  _displayCart();

}

function _displayCount() {
  let i = 0
  cart.forEach(item => { i = i + item.quantity; });

  const name = (i === 1) ? ' item in cart' : ' items in cart';
  $('#lblMessage').text(`${i} ${name}`);
}

function _displayItems(data) {
  const tBody = document.getElementById('tblProductsBody');
  tBody.innerHTML = '';

  const button = document.createElement('button');

  data.forEach(item => {

    let addButton = button.cloneNode(false);
    addButton.innerText = 'Add To Cart';
    addButton.setAttribute('onclick', `addItemToCart(${item.id})`);
    addButton.setAttribute('class', 'btn btn-secondary')

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.name);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(item.description);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    let textNode3 = document.createTextNode(`$${item.price}`);
    td3.appendChild(textNode3);

    let td4 = tr.insertCell(3);
    td4.appendChild(addButton);
  });
}

function updateQuantity(id, obj) {
  if (Number(obj.value) === 0) {
    removeItemFromCart(id);
  }
  else {
    cart.forEach(v => {

      if (v.id === id) {
        v.quantity = Number(obj.value);

      }
      _displayCount();
      _displayCart();
    });
  }
}

function _displayCart() {
  const tBody = document.getElementById('tblCartBody');
  tBody.innerHTML = '';

  const button = document.createElement('button');
  const textBox = document.createElement('input');
  let totalPrice = 0;

  cart.forEach(item => {
    totalPrice += item.price;

    let delButton = button.cloneNode(false);
    delButton.innerText = 'Delete';
    delButton.setAttribute('onclick', `removeItemFromCart(${item.id})`);
    delButton.setAttribute('class', 'btn btn-secondary');

    let updateQty = textBox.cloneNode(false);
    updateQty.setAttribute('type', 'text');
    updateQty.value = item.quantity;
    updateQty.setAttribute('onchange', `updateQuantity(${item.id}, this)`);
    //addButton.setAttribute('class', '');

    let tr = tBody.insertRow();

    let td1 = tr.insertCell(0);
    let textNode = document.createTextNode(item.name);
    td1.appendChild(textNode);

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(`$${item.price * item.quantity}`);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    td3.appendChild(updateQty);

    let td4 = tr.insertCell(3);
    td4.appendChild(delButton);
  });

  let tr = tBody.insertRow();
  let td = tr.insertCell(0);
  let textNode = document.createTextNode("TOTAL");
  td.appendChild(textNode);

  let td1 = tr.insertCell(1);
  let textNode1 = document.createTextNode(`$${totalPrice}`);
  td1.appendChild(textNode1);


}

