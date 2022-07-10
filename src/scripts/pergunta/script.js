$(document).ready(function () {
    //alert(LerCookie('nome'));
    Funcoes.Inicio();

    $(".question").on('click', function () {
        Click.SelecionarResposta($(this));
    });
});


var respostaCerta;
var ultimaQuestao = false;
var quantCerta = 0;
var indice = 0;
var materia = 0;
var questao = 0;
var resultado = 0;


Funcoes = {
    /**
     * Recuperar todos os parâmetro e chamar o Load para fazer a perguntar
     */
    Inicio: function () {
        var parameter = window.location.search.replace("?", "");
        var parameters = parameter.split("&");

        indice = parameters[0].replace("i=", "");
        materia = parameters[1].replace("m=", "");
        questao = parseInt(parameters[2].replace("q=", ""));
        resultado = parseInt(parameters[3].replace("r=", ""));
        var url = '';


        var arrayQuestion = Questoes[indice][materia];
        $("#resultado").html(resultado);
        $("#materia").html(Questoes[indice][materia - 1])
        var questaoAtual = parseInt(questao + 1) + "/" + arrayQuestion.length;

        $("#qAtual").html(questaoAtual);

        /**
         * Verificar se a última questão;
         */
        if (arrayQuestion.length - 1 === questao) {
            ultimaQuestao = true;
            $("#avanca").html('Finalizar');
        }

        /**
         * Definer a url
         */
        if (!ultimaQuestao)
            url = window.location.pathname + "?i=" + indice + "&m=" + materia + "&q=" + parseInt(questao + 1);
        else
            url = "resultado.html?i=" + indice + "&m=" + parseInt(materia - 1) + "&qf=" + parseInt(questao + 1) +
                "&c=" + resultado;

        /**
         * Set os click no button
         */
        $("#pula").on('click', function () {
            Click.pularBtn(url + "&r=" + parseInt(resultado));
        });
        $("#avanca").on('click', function () {
            if (!ultimaQuestao)
                Click.avancaBtn(url + "&r=" + parseInt(resultado + quantCerta));
            else
                Click.avancaBtn(url);
        });
        $("#desistir").on('click', function () {
            Click.desistirBtn("resultado.html?i=" + indice + "&m=" + parseInt(materia - 1) +
                "&qf=" + parseInt(questao + 1) + "&c=" + resultado);
        });

        // Passar URL para se acabar o tempo o relógio mudar de questão.
        Load.LoadWindows(indice, materia, questao, url);
    },
    /**
     * Fazer o redirecionamento
     * @param {string} url 
     */
    mudaQuestion: function (url) {
        $(window.document.location).attr('href', url);
    }
};


Load = {
    Question: function (questionNumber, question) {
        /**
         * [] -> Questão
         * [] -> Pergunta
         */
        $("#question").html('<span class="number">' + parseInt(questionNumber + 1) + '. </span>' + question[questionNumber][0]);

        $("#question").css('font-family', 'Roboto');
        $("#question").css('font-size', 'larger');
        $("#question").css('color', '#199BBE');

        $(".number").css('color', '#14738C');
        $(".number").css('font-weight', 'bold');

        /**
         * [] -> Questão
         * [] -> Array com as Respostas
         */
        var resposta = question[questionNumber][1];
        var html = "";

        /**
         * Faz um random para a resposta certa;
         */
        respostaCerta = Math.floor((Math.random() * resposta.length));

        var j = 1;
        var letras = ['A  ', 'B  ', 'C  ', 'D  ', 'E  '];

        for (i = 0; i < resposta.length; i++) {

            if (respostaCerta === i) {
                html += '<p class="thumbnail text-left question" ' +
                    'style="background: #199BBE; color: white; cursor: pointer;" id="' + i + '">' +
                    '<span style="color: white;"><i><b>' + letras[i] + ' - </b></i></span>' + resposta[0] + '</p>';
            } else {
                html += '<p class="thumbnail text-left question" ' +
                    'style="background: #199BBE; color: white; cursor: pointer; " id="' + i + '">' +
                    '<span style="color: white; backgroundImage: "><i><b>' + letras[i] + ' - </b></i></span>' + resposta[j] + '</p>';
                j++;
            }
        }
        $("#resposta").html(html);
    },
    /**
     * Tempo para as questões
     * @param {string} time
     * @param {string} url 
     */
    relogio: function (time, url) {
        time = parseInt(time);

        $("#tempo").html(time);
        time--;

        var tempo = setInterval(function () {
            $("#tempo").html(time);
            if (time === 0) {
                clearTimeout(tempo);
                alert('Tempo se esgotou, Você será levada a próxima questão');
                Funcoes.mudaQuestion(url + "&r=" + parseInt(resultado));
            }
            time--;
        }, 1000);
    },
    /**
     * Depois de toda a separação dos parâmetros, essa função e chamada para
     * inicia;
     * @param {int} indice
     * @param {int} materia
     * @param {int} questao
     * @param {string} url
     */
    LoadWindows: function (indice, materia, questao, url) {
        /**
         * [] -> letra
         * [] -> Matéria
         */
        var arrayQuestion = Questoes[indice][materia];

        Load.Question(questao, arrayQuestion);
        Load.relogio(60, url);
    }
};

Click = {
    SelecionarResposta: function (elemento) {
        /**
         * Limpar todas as respostas
         */
        $(".question").attr("check", false);
        $(".question").css("background-color", "#199BBE");
        $(".question").css("border-color", "");

        /**
         * Selecionar a resposta clicada
         */
        $(elemento).attr("check", true);
        $(elemento).css("background-color", "#BE0000");
        $(elemento).css("border-color", "#820000");

        if ($(elemento).attr("id") == respostaCerta) {
            quantCerta = 1;
        } else {
            quantCerta = 0;
        }

        $("#avanca").attr("disabled", false);
        $("#avanca").css("background-color", "#005A00");
    },
    /**
     * Executa o click do botão avançar
     * @param {type} url - url a transferida
     */
    avancaBtn: function (url) {
        $(".question").each(function () {
            if ($(this).attr("id") == respostaCerta) {
                $(this).css("background-color", "#005A00");
            }
        });
        $(".question").each(function () {
            if ($(this).attr("check") == "true") {
                if ($(this).attr("id") == respostaCerta)
                    console.log('Parabéns você acertou');
                else
                    console.log('Infelizmente você errou, a resposta certa está em verde');
            }
        });
        Funcoes.mudaQuestion(url);
    },
    pularBtn: function (url) {
        var resposta = confirm('Deseja realmente pular essa questão?');
        if (resposta)
            Funcoes.mudaQuestion(url);
    },
    desistirBtn: function (url) {
        var resposta = confirm('Deseja realmente encerrar a sessão?');
        if (resposta)
            Funcoes.mudaQuestion(url);
    }
};