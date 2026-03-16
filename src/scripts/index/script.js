/* global Questões */

$(document).ready(function () {
    Load.Indice();


});


Load = {
    Token: 'eyJhbGciOiJSUzI1NiIsImtpZCI6ImUzZWU3ZTAyOGUzODg1YTM0NWNlMDcwNTVmODQ2ODYyMjU1YTcwNDYiLCJ0eXAiOiJKV1QifQ.eyJuYW1lIjoiRWR2YWxkbyBGYXJpYXMiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tL2EvQUNnOG9jS0ItMDV5cFNtSXlzMlZhZ3BwSE9UaVNmc3JqWTdGX0w3WFkwTGMtZDlrZHh0aHR3TVN1dz1zOTYtYyIsImlzcyI6Imh0dHBzOi8vc2VjdXJldG9rZW4uZ29vZ2xlLmNvbS9xdWl6LWVkdmFsZG9mYXJpYXMiLCJhdWQiOiJxdWl6LWVkdmFsZG9mYXJpYXMiLCJhdXRoX3RpbWUiOjE3NTcxNzgwNjUsInVzZXJfaWQiOiIzQzlvWlpWaTE5Y2V5Umw4WW9GNmsxU005SG8yIiwic3ViIjoiM0M5b1paVmkxOWNleVJsOFlvRjZrMVNNOUhvMiIsImlhdCI6MTc1NzM1NDQ0NSwiZXhwIjoxNzU3MzU4MDQ1LCJlbWFpbCI6ImVkdmFsZG9mYXJpYXMuc2FudGFuYUBnbWFpbC5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJnb29nbGUuY29tIjpbIjExNjAyNDk4NjgzMTI1MjczNTc4NCJdLCJlbWFpbCI6WyJlZHZhbGRvZmFyaWFzLnNhbnRhbmFAZ21haWwuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoiZ29vZ2xlLmNvbSJ9fQ.EsfOQ-xzzxiXdzZWTxSpXuiqalTXirrsJZlKpd03m4X7R4lnLuFq0jmlbPF--lxmhb1XWJ2hqCTuQVTpUTG7KOkX6zI4diB_GsH56HyP20acU4vgIjMSDRJAfj4adQmqEfwQDq_x7m-9h9bjh1Xvf8h2Fa9Fe80LZS832T3SzmEAIJJ4YWcLfzc40boypSmkvDtIsit5I1uIUAajCDs7aew90R799JvqtzLOgnAP1-izxQnAX2QeysTOoueW2DYRGfqwLE9fCyg-XOyrbm_6uDlAH6TIY24ImZQh9nbi9ERCPeUOyq-5JP6yD4mYPtp1p_jwjRDlTe8_w0C-YckFQg',

    /**
     * Carregar o índice
     */
    Indice: async function () {
        const res = await fetch('http://localhost:5107/Subject/Initials', {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${Load.Token}`,
                "Content-Type": "application/json"
            }
        });

        const data = await res.json();
        console.log(data.initials);
        var initials = data.initials;


        var count = Questoes.length;
        var html = "";

        for (i = 0; i < initials.length; i++) {

            html += '<div class="col-md-1 thumbnail text-center btn btn-default button" id="' + initials[i] +
                '" style="margin: 2px; border-color: #3bc4e6; background: #199BBE; color: white;">' +
                initials[i] + '</div>';

        }

        // for (i = 0; i < count; i += 2) {
        //     if (Questoes[i + 1] !== null) {
        //         html += '<div class="col-md-1 thumbnail text-center btn btn-default button" id="' + i +
        //             '" style="margin: 2px; border-color: #3bc4e6; background: #199BBE; color: white;">' +
        //             Questoes[i] + '</div>';
        //     }
        // }

        $('#linhas').html(html);

        $(".button").on('click', function () {
            var id = $(this).attr('id');
            Load.Materia(id);
            //GerarCookie('nome', 'informações', 0);
        });
    },
    /**
     * Carregar todas as matérias que tem perguntas
     * @param {id} id numero do array que contém ás matérias que será carregada.
     */
    Materia: async function (id) {
        const res = await fetch('http://localhost:5107/Subject/names?initial=' + id, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${Load.Token}`,
                "Content-Type": "application/json"
            }
        });

        const data = await res.json();
        console.log(data.names);
        var names = data.names;

        let html = "";
        for (i = 0; i < names.length; i++) {
            html += '<a href="pergunta.html?i=' + id + '&m=' + parseInt(i + 1) + '&q=0&r=0">' +
                '<div class="col-md-4 thumbnail text-center btn btn-info button"' +
                'style="margin: 2px; border-color: #3bc4e6; color: #14738C;">' +
                names[i] + '</div></a>';
        }


        $('#materias').html(html);
        $('.materias').show();


        //Espaçamento forçado via jQuery
        var tamanho = $('.col-md-4').width();
        $('.col-md-4').width(tamanho - 4);
    }
};
