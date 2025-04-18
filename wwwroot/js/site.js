// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$('.title').text('Новый текст')

$('#changeColor').on('click', function () {
    $('.title').css({'color' : 'red'})
})

$('#addText').on('click', function () {
    $('.text-center').append('<p>Новая строка</p>')
})

$(window).on('scroll', function () {
    if (window.scrollY > 150) {
        $('nav').addClass('scrolled')
    }
    else {
        $('nav').removeClass('scrolled')
    }
})

$(document).on('click', '.nav-link[data-page="catalog"]', function (e) {
    e.preventDefault();
    ShowCatalogPage();
})

$(document).on('click', '.nav-link[data-page="add_car"]', function (e) {
    e.preventDefault();
    ShowAddNewCarPage();
})

$(document).on('click', '.nav-link[data-page="cart"]', function (e) {
    e.preventDefault();
    ShowCartPage();
})

$(document).on('click', '.nav-link[data-page="edit_orders"]', function (e) {
    e.preventDefault();
    ShowOrdersPage();
})

const ShowOrdersPage = () => {
    $('main').html(
        `<div class="page_index">
        <h1>Записи пользователей</h1>
        <p>Здесь будут отображаться текущие записи пользователей</p>
        <p>Редактируйте статус записи</p>
        <div class="orders_table">
        </div>
        </div>`)
    ShowOrders()
}
/*передача по .ajax оформленной пользователем записи на странице Записей в личном кабинете Администратора*/
const ShowOrders = () => {
    $.ajax({
        method: 'GET',
        url: 'api/order'

    }).done((data) => {
        console.log(data)
        let list = ''

        $.each(data, function () {
        list +=
            `<div class="cart_grid" data-id="${this.id}">
            <div class="cart_item">
                <h4 class="item_title_cart">Номер записи: ${this.id}</h4>
                <h4 class="item_title_cart">Пользователь: ${this.user.userName}</h4>
                <p class="item_title_cart">Статус: ${this.status}</p>
            </div>`
        if (this.status == 'Создана') list += `<button class="item_add-btn" data-action="change_status" data-id="${this.id}">Изменить статус</button>`

            list += `</div>`
        })

        $('.orders_table').html(list)
    })
}

$(document).on('click', 'button[data-action="change_status"]', function () {
    orderId = $(this).attr('data-id')

    $.ajax({
        method: 'PUT',
        url: 'api/order',
        data: orderId,
        contentType: 'application/json'
    }).done(() => {
        ShowOrders()
    })
})

//Обработчик нажатия на кнопку поиска
$(document).on('click', 'button[data-id="search_catalog-btn"]', function () {
    let query = $('input[data-id="search_catalog-input"]').val();
    SearchCatalog(query)
})

const ShowCatalogPage = () => {
    $('main').html(
        `<div class="page_index">
        <h1>Каталог услуг</h1>
        <p>Наша команда бьюти-экспертов предоставляет Вам широкий ассортимент услуг индустрии красоты</p>
        <input class="input_item" data-id="search_catalog-input" placeholder="Поиск">
        <button class="item_add-btn" data-id="search_catalog-btn">Поиск</button>
        <div id="catalog_grid"></div>
        </div>`
    )
    SearchCatalog();
}

const ShowAddNewCarPage = () => {
    $('main').html(
        `<div class="page_index">
        <h1>Добавление новой услуги</h1>
        <p>Здесь Вы можете добавлять новые услуги в каталог</p>
        <div class="cart_grid">
        <input class="input_item-new" data-id="add_new_car_name" placeholder="Название">
        <textarea class="input_item-new" data-id="add_new_car_description" placeholder="Описание"></textarea>
        <input class="input_item-new" data-id="add_new_car_price" placeholder="Цена">
        <button class="item_add-btn" data-action="add_new_car">Добавить</button>
        </div>
        </div>`
    )
}

const ShowCartPage = () => {
    $('main').html(
        `<div class="page_index">
        <h1>Корзина услуг</h1>
        <p>Оформите запись на интересующие Вас услуги и с Вами свяжется наш менеджер для консультаци</p>
        <div data-id="cart_table">
        </div>
        </div>`
    )
    ShowCartList()
}

const ShowCartList = () => {

    $.ajax({
        method: 'GET',
        url: 'api/cart'
    }).done((data) => {
        console.log(data)
        let list = ''

        $.each(data, function () {
            list +=
                `<div class="cart_grid" data-id="${this.cars.id}">
                   <div class="cart_item">
                <h4 class="item_title_cart">${this.cars.name}</h4>
                <h4 class="item_title_cart">${this.quantity}</h4>
                    <div class="btn_add-remove" data-id="${this.cars.id}">
                      <button class="item_add-btn" data-action="cart_add_car">Добавить</button>
                      <button class="item_add-btn" data-action="cart_remove_car">Удалить</button>
                    </div>
                   </div>
                </div>`
        })

        list += '<button class="item_add-btn" data-action="make_order">Оформить запись</button>'

        $('div[data-id="cart_table"]').html(list)
    })
}
/*обработчик нажатия на кнопку оформления записи*/
$(document).on('click', 'button[data-action="make_order"]', function () {
    $.ajax({
        method: 'POST',
        url: 'api/order',
        contentType: 'application/json'

    }).done(function () {
        ShowCartList()
    })
})
/*обработчик нажатия на кнопку добавления новой услуги*/
$(document).on('click', 'button[data-action="add_new_car"]', function () {
    let name = $('input[data-id="add_new_car_name"]').val()
    let description = $('textarea[data-id="add_new_car_description"]').val()
    let price = $('input[data-id="add_new_car_price"]').val()

    $.ajax({
        method: 'POST',
        url: 'api/admin/addnewcar',
        contentType: 'application/json',
        data: JSON.stringify({
            name,
            description,
            price
        })
    })
})
/*обработчик нажатия на кнопку добавления в корзину*/
$(document).on('click', 'button[data-action="catalog_add_car"]', function () {
    let carId = $(this).parent().attr('data-id');
    $.ajax({
        url: '/api/cart',
        method: 'POST',
        data: carId,
        contentType: 'application/json'
    })
})


$(document).on('click', 'button[data-action="cart_add_car"]', function () {
    let carId = $(this).parent().attr('data-id');
    $.ajax({
        url: '/api/cart',
        method: 'POST',
        data: carId,
        contentType: 'application/json'

    }).done(function () {
        ShowCartList();
    })
})

$(document).on('click', 'button[data-action="cart_remove_car"]', function () {
    let carId = $(this).parent().attr('data-id');
    $.ajax({
        url: '/api/cart/remove',
        method: 'POST',
        data: carId,
        contentType: 'application/json'

    }).done(function () {
        ShowCartList();
    })
})

const SearchCatalog = (query) => {
    $('#catalog_grid').html('');
    $.ajax({
        url: 'api/catalog',
        method: 'GET',
        data: { query }

    }).done(function (data) {
        console.log(data)

        $.each(data, function () {
            $('#catalog_grid')
                .append(
                    `<div class="catalog_item" data-id="${this.id}">
                    <div class="item_img"><img src="https://avatars.mds.yandex.net/get-images-cbir/1113758/YI9769kQ-_kEuhDoNnL6UQ3848/ocr"></div>
                    <h4 class="item_title">${this.name}</h4>
                    <div class="item_description">
                    ${this.description}
                    </div>
                   <h3 class="item_price">${this.price}₽</h3>
                   <button class ="item_add-btn" data-action="catalog_add_car">Добавить в корзину</button>
                </div>`
                )
        })
    })
}


// Write your JavaScript code.
