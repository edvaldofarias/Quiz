$(document).ready(function () {
    var parametro = window.location.search.replace("?", "");
    parametro = parametro.split("&");

    indice = parseInt(parametro[0].replace("i=", ""));
    materia = parseInt(parametro[1].replace("m=", ""));
    questoesFeitas = parseInt(parametro[2].replace("qf=", ""));
    questoesCertas = parseInt(parametro[3].replace("c=", ""));

    Load.Page(indice, materia, questoesFeitas, questoesCertas);
});

Load = {
    Page: function (indice, materia, feitas, certas) {
        var numeroQuestao = Questoes[indice][parseInt(materia + 1)].length;
        var erradas = feitas - certas;
        var porcetagem = (100 / feitas) * certas;
        
        $("#materia").html(Questoes[indice][materia]);
        $("#questaoMateria").html(numeroQuestao);
        $("#questaoFeita").html(feitas);
        $("#questaoError").html(erradas);
        $("#questaoCerta").html(certas);
        $("#porcetagem").html(porcetagem.toFixed(2) + "%");
    }
};