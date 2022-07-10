/* global Questoes */

$(document).ready(function () {
    Load.Indice();

    $(".button").on('click', function () {
        var id = parseInt($(this).attr('id')) + 1;
        Load.Materia(id);
	//GerarCookie('nome', 'informações', 0);
    });
});


Load = {
    /**
     * Carregar o índice
     */
    Indice: function () {
        var count = Questoes.length;
        var html = "";

        for (i = 0; i < count; i += 2) {
            if (Questoes[i + 1] !== null) {
                html += '<div class="col-md-1 thumbnail text-center btn btn-default button" id="' + i + 
				'" style="margin: 2px; border-color: #3bc4e6; background: #199BBE; color: white;">' +
                        Questoes[i] + '</div>';
            }
        }

        $('#linhas').html(html);
    },
    /**
     * Carregar todas as matérias que tem perguntas
     * @param {id} id numero do array que contém ás matérias que será carregada.
     */
    Materia: function (id) {
        var array = Questoes[id];

        var count = array.length;
        var html = "";

        for (i = 0; i < count; i += 2) {
            if (array[i + 1] !== null) {
                html += '<a href="pergunta.html?i=' + id + '&m=' + parseInt(i + 1) + '&q=0&r=0">' +
                        '<div class="col-md-4 thumbnail text-center btn btn-info button"'+
						'style="margin: 2px; border-color: #3bc4e6; color: #14738C;">' +
                        array[i] + '</div></a>';
            }
        }
		
		$('#materias').html(html);
        $('.materias').show();
		
		
		//Espaçamento forçado via jQuery
		var tamanho = $('.col-md-4').width();
		$('.col-md-4').width(tamanho - 4);
    }
};
