// Adicionamos o método date para sobrepor o método padrão e atribuindo sempre um retorno verdadeiro para garantir 
// que o JQuery não vai tratar as data exibindo mensagens indesejadas. 
$.validator.addMethod('date',
   function (value, element) {
       return true;
   });

// Aqui adicionamos os métodos respeitando os nomes gerado em nossa classe atributo
// os nomes devem estar em caixa baixa tanto na integração como aqui no JavaScript (Nome atributo, parametro)
$.validator.unobtrusive.adapters.addSingleVal("databrasil", "datarequerida");

//Aqui temos a funcao que Valida do lado do cliente as datas digitadas utilizando uma expressão regurar
$.validator.addMethod('databrasil',
function (value, element, datarequerida) {

    // Verificamos se o valor foi digitado
    if (value.length == 0) {
        // Se a data é requerida retorna erro senão retorna com sucesso
        if (datarequerida.toString() == 'True') {
            return false;
        }
        else {
            return true;
        }
    }

    /// Expressão Regular para tratar datas, dd/MM/yyyy, validando também ano Bisexto.
    var expReg = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;

    // Valida a expressão, se for compatível vai retornar validando o campo, caso contrario exibe a mensagem de erro informada no atributo;
    return value.match(expReg);
});

// Validações Numeros

// Adicionamos o método ranger para sobrepor o método original e corrigir os valores passados,
// retirando o ponto milhar e trocando a virgula decimal por ponto decimal, 
// Com isto solucionamos os conflitos ao tentar definir um range para um valor quando utilizamos o atributo NumeroBrasil 
$.validator.addMethod('range',
   function (value, element, params) {

    // Coleta os parametros passados, valor maximo e minimo
    var min = params[0];
    var max = params[1];

    // Retira todo ponto de milhar formatado no campo valor
    var pos = value.indexOf('.');
    while (pos > -1) {
        value = value.replace('.', '');
        pos = value.indexOf('.');
    }

    // Troca virgula decimal por ponto decimal
    value = value.replace(',', '.');

    // Faz a validacao do valor, comparando com o valor minimo e maximo
    if (value < min) {
        return false;
    }

    if (value > max) {
        return false;
    }

    return true;
});


// Adicionamos o metodo number para sobrepor o metodo padrão e atribuindo sempre um retorno verdadeiro para garantir 
// que o JQuery não vai tratar os numeros exibindo mensagens indesejadas. 
$.validator.addMethod('number',
   function (value, element) {
    return true;
});

// Aqui adicionamos os metodos respeitando os nomes gerado em nossa classe atributo
// os nomes devem estar em caixa baixa tanto na integração como aqui no JavaScript (Nome atributo, parametro)
$.validator.unobtrusive.adapters.addSingleVal('numerobrasil', 'params');

//Aqui temos a funcao que Valida do lado do cliente os números digitados utilizando uma expressão regurar
$.validator.addMethod('numerobrasil',
function (value, element, params) {

    // Separamos o parametros recebidos
    var parametros = params.split(',');

    // Verifica o Ponto de milhar
    if (parametros[1].toString() == "True") {
        var pos = value.indexOf('.');
        while (pos > -1) {
            value = value.replace('.', '');
            pos = value.indexOf('.');
        }
    }

    // Valida caso o valor não tenha sido preenchido
    if (value.length == 0) {
        return true;
    }

    var expReg = '';

    // atribui a expressao regular com ou sem ponto decimal
    if (parametros[0].toString() == 'False') {
        expReg = /^[-+]?\d*$/;
    }
    else {
        expReg = /^[-+]?\d*\,?\d*$/;
    }

    // Valida a expressão, se for compativel vai retornar validando a campo, caso contrario exibe a mensagem de erro informada no atributo;
    return value.match(expReg);
});


// Email

// Aqui adicionamos os metodos respeitando os nomes gerado em nossa classe atributo
// os nomes devem estar em caixa baixa tanto na integração como aqui no JavaScript (Nome atributo, parametro)
$.validator.unobtrusive.adapters.addSingleVal('emailbrasil', 'emailrequerido');

//Aqui temos a funcao que Valida do lado do cliente o e-mail digitado utilizando uma expressão regular
$.validator.addMethod('emailbrasil',
function (value, element, emailrequerido) {

    // Valida caso o valor não tenha sido preenchido
    if (value.length == 0) {
        if (emailrequerido.toString() == "True") {
            return false;
        }
        else {
            return true;
        }
    }

    // Atribui expressão
    var expReg = /^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$/;

    // Valida Expressao
    var result = value.match(expReg);

    // Retorna false caso a expressao nao seja valida
    if (result != null) {
        return true;
    }

    return false;
});


// Senha

// Aqui adicionamos os metodos respeitando os nomes gerado em nossa classe atributo
// os nomes devem estar em caixa baixa tanto na integração como aqui no JavaScript (Nome atributo, parametro)
$.validator.unobtrusive.adapters.addSingleVal('senhabrasil', 'params');

//Aqui temos a funcao que Valida do lado do cliente a senha digitadas utilizando uma expressão regular
$.validator.addMethod('senhabrasil',
function (value, element, params) {

    // Separamos os parametros recebidos
    var parametros = params.split(';');

    // Definimos o tamanho maximo com base em parametro recebido      
    var maximo = parametros[1].toString();

    // Verifica se o tamanho maximo foi atingido
    if (value.length > maximo) {
        return false;
    }


    // Recupera expressao regular com base em parametro recebido
    var expReg = parametros[0];

    // Valida Expressao
    var result = value.match(expReg);

    // Retorna false caso a expressao nao seja valida
    if (result != null) {
        return true;
    }

    return false;
});