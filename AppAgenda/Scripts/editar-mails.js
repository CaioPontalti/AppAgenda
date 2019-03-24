$(function () {

    var indexMail = $("#div-Mail .row").length;

    $("#btn-add-Mail").click(function (e) {
        e.preventDefault(); //não envia o form ao clicar no botão

        var addMailHtml =
            '<div class="row">' +
            '<div class="col-md-5">' +
            '<input type="text" name="Emails[' + indexMail + '].Descricao" class="form-control txt-Mail" placeholder="Digite o Email" />' +
            '</div>' +
            '<div class="col-md-1">' +
            '<button class="btn btn-danger btn-remover-Mail">' +
            '<span class="glyphicon glyphicon-trash"></span>' +
            '</button>' +
            '</div>' +
            '</div>';

        $("#div-Mail").append(addMailHtml); // add todo o HTML criado acima armazenado na variavel dentro da div "#div-Mail"

        indexMail++;
    });


    //Quando ocorrer um click, em um elemento com a classe .btn-remover-Mail, dentro da div #div-Tel => A função é executada.
    $("#div-Mail").on('click', '.btn-remover-Mail', function (e) {
        e.preventDefault();


        var id = $(this).attr("data-id"); //pega o id na view de editar => data-id="@Model.Mail[i].Id"

        if (id) {
            $.post("/Emails/DeletarMails?id=" + id);
        }


        //this é botão, .parente é o pai do botão, .parent é o pai do pai do botão => a linha com o telefone. <div class="row"
        $(this).parent().parent().remove();

        indexMail--;

        //Cada vez que clicar para remover um telefone, ele faz um foreach reorganizando os indices dos que ficaram na tela.
        $('#div-Mail .row').each(function (indice, elemento) {
            $(elemento).find('.txt-Mail').attr('name', "Emails[" + indexMail + "].Descricao");
        });
    });

});