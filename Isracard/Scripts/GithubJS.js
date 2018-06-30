function AddToBookMark(id, name, avatar, url) {
    $.ajax({
        url: 'AddBookMark',
        data: { ID: id, Name: name, Avatar: avatar, Url: url }
    }).done(function (response) {
        alert(response);
    });
}