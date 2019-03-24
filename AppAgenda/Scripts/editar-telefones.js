$(function () {

    //Retorna a quantidade de linhas que tem dentro da div de telefones, para que quando for add um número pela edição
    // ele não comece do index 0, e sim da quantidade de telefones na tela.
    var indexTelefone = $("#div-Tel .row").length;

    $("#btn-add-Tel").click(function (e) {
        e.preventDefault(); //não envia o form ao clicar no botão

        var addTelefoneHtml =
            '<div class="row">' +
            '<div class="col-md-2">' +
            '<input type="text" name="Telefones[' + indexTelefone + '].DDD" class="form-control txt-ddd" placeholder= "DDD" onkeypress = "onlynumber()"  maxlength="2" style="text-align:center" required />' +
            '</div>' +
            '<div class="col-md-6">' +
            '<input type="text" name="Telefones[' + indexTelefone + '].Numero" class="form-control txt-numero"  placeholder= "Número do Telefone" onkeypress = "onlynumber()"  maxlength="10"  style="text-align:center"  required />' +
            '</div>' +
            '<div class="col-md-3">' +
            '<select name="Telefones[' + indexTelefone + '].Tipo" class="form-control sel-tipo">' +
            '<option value="0">Residencial</option>' +
            '<option value="1">Comercial</option>' +
            '<option value="2">Celular</option>' +
            '</select>' +
            '</div>' +
            '<div class="col-md-1">' +
            '<button class="btn btn-danger btn-remover-telefone">' +
            '<span class="glyphicon glyphicon-trash"></span>' +
            '</button>' +
            '</div>' +
            '</div>';

        $("#div-Tel").append(addTelefoneHtml); // add todo o HTML criado acima armazenado na variavel dentro da div "#div-Tel"

        indexTelefone++;
    });


    //Quando ocorrer um click, em um elemento com a classe .btn-remover-telefone, dentro da div #div-Tel => A função é executada.
    $("#div-Tel").on('click', '.btn-remover-telefone', function (e) {
        e.preventDefault();

        var id = $(this).attr("data-id"); //pega o id na view de editar => data-id="@Model.Telefones[i].Id"

        if (id)
        {
            $.post("/Telefones/DeletetarTelefone?id=" + id); 
        }

        //this é botão, .parente é o pai do botão, .parent é o pai do pai do botão => a linha com o telefone. <div class="row"
        $(this).parent().parent().remove();

        indexTelefone--;

        //Cada vez que clicar para remover um telefone, ele faz um foreach reorganizando os indices dos que ficaram na tela.
        $('#div-Tel .row').each(function (indice, elemento) {
            $(elemento).find('.txt-ddd').attr('name', "Telefones[" + indexTelefone + "].DDD");
            $(elemento).find('.txt-numero').attr('name', "Telefones[" + indexTelefone + "].Numero");
            $(elemento).find('.sel-tipo').attr('name', "Telefones[" + indexTelefone + "].Tipo");
        });
    });

});