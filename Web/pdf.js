module.exports = function (callback, customer, vehicle, schedule) {
    const jsreport = require('jsreport-core')();
    const fs = require('fs');

    schedule.ExpectedWithdrawalDate = new Date(Date.parse(schedule.ExpectedWithdrawalDate))
        .toLocaleString('pt-BR', { timeZone: 'America/Sao_Paulo' });

    schedule.ExpectedReturnDate = new Date(Date.parse(schedule.ExpectedReturnDate))
        .toLocaleString('pt-BR', { timeZone: 'America/Sao_Paulo' });

    schedule.HourlyPrice = schedule.HourlyPrice.toString().replace(".", ",");

    jsreport.init().then(function () {
        return jsreport.render({
            template: {
                content: fs.readFileSync('vehicle-rental-template.html').toString(),
                engine: 'jsrender',
                recipe: 'chrome-pdf',
                chrome: {
                    landscape: true
                }
            },
            data: {
                customer,
                vehicle,
                schedule
            }
        }).then(function (resp) {
            callback(null, resp.content.toJSON().data);
        });
    }).catch(function (e) {
        callback(e, null);
    })
};
