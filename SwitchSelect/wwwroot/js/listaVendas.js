function atualizarStatus(pedidoId, novoStatus) {
    $.post('/Admin/AtualizarStatus', { pedidoId: pedidoId, novoStatus: novoStatus })
        .done(function (data) {
            // Fechar o modal automaticamente após 1 segundo
            setTimeout(function () {
                $('#atualizarStatusModal-' + pedidoId).modal('hide');
            }, 250);

            setTimeout(function () {
                location.reload();
            }, 250);
        })
        .fail(function (xhr, status, error) {
            console.error('Erro ao atualizar o status do pedido:', error);
        });
}