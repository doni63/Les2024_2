function onSubmitForm() {
    var total = parseFloat(document.querySelector('input[name="total"]').value);
    var valorTotalCartoes = 0;
    var valorInvalido = false;
    var pagamentoAbaixoDezReaisPermitido = document.getElementById('pagamentoAbaixoDezReaisPermitido').value === 'True';

    // Verifica se um endereço foi selecionado
    var enderecoSelecionado = document.getElementById('endereco').value;
    if (!enderecoSelecionado) {
        alert("Por favor, selecione um endereço.");
        return false;
    }

    // Calcula o valor total dos cartões selecionados
    var cartoesSelecionados = document.querySelectorAll('input[name="cartoesSelecionados"]:checked');
    cartoesSelecionados.forEach(function (cartao) {
        var cartaoId = cartao.value;
        var valor = parseFloat(document.querySelector('input[name="valorCartao_' + cartaoId + '"]').value);

        if (isNaN(valor)) {
            valorInvalido = true;
        }
        else {
            valorTotalCartoes += valor;
        }
        //verifica se um cartão selecionado é menor que R$10,00
        if (valor < 10) {
            valorInvalido = true;
        }

    });
    //verifica se pedidototal é menor que R$10,00, valorInvalido recebe false para permitir valor menor que 10 reais
    if (pagamentoAbaixoDezReaisPermitido) {
        valorInvalido = false;
    }

    //verifica se tem cartão com menos de R$10,00
    if (valorInvalido) {
        alert("O valor mínimo no cartão é de R$10,00")
        return false;
    }

    // Verifica se o valor total dos cartões é menor que o valor total do pedido
    if (valorTotalCartoes < total) {
        alert("O valor total dos cartões não pode ser menor que o valor total do pedido.");
        return false; // Impede o envio do formulário
    }

    var cartoesIds = [];
    cartoesSelecionados.forEach(function (cartao) {
        var cartaoId = cartao.value;
        var valor = document.querySelector('input[name="valorCartao_' + cartaoId + '"]').value;
        cartoesIds.push({ id: cartaoId, valor: valor });
    });
    document.getElementById('cartoesIds').value = JSON.stringify(cartoesIds);
    return true; // Permite o envio do formulário
}

function calcularFrete(cep) {
    fetch('/Frete/CalcularFrete', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ cep: cep })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                var freteInput = document.getElementById('frete');
                freteInput.value = data.valorFrete;

                // Atualiza o valor total
                var precoTotalPedidoString = document.querySelector('input[name="precoTotalPedido"]').value;
                var precoTotalPedido = parseFloat(precoTotalPedidoString.replace(',', '.'));

                var valorFrete = parseFloat(freteInput.value);
                var totalInput = document.getElementById('total');

                // Se o valor do pedido é zero, manter o total como zero
                if (precoTotalPedido === 0) {
                    totalInput.value = "0,00";
                } else {
                    totalInput.value = ((precoTotalPedido) + valorFrete).toFixed(2).replace('.', ',');
                }
            } else {
                alert("Não foi possível calcular o frete. Tente novamente.");
            }
        })
        .catch(error => {
            console.error('Erro ao calcular o frete:', error);
        });
}
document.getElementById('endereco').addEventListener('change', function () {
    var selectedOption = this.options[this.selectedIndex];
    if (selectedOption.value) {
        var cep = selectedOption.getAttribute('data-cep');
        calcularFrete(cep);
    } else {
        document.getElementById('frete').value = '';
        document.getElementById('total').value = '';
    }
});

function validarAplicacaoCupom() {
    var totalInput = document.getElementById('totalInput');
    var total = parseFloat(totalInput.value.replace('.', ','));

    if (total <= 0) {
        alert('O valor total já é zero. Não é possível aplicar outro cupom.');
        return false; 
    }

    return true; 
}










