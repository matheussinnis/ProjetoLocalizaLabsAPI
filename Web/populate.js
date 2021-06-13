// let fs = require('fs');
// let path = require('path');
//
// let agencias = JSON.parse(
//     fs.readFileSync('./belo horizonte.json').toString()
// )[0].agencias;

let agencias = JSON.parse(`[
  {
    "codigoPais": "0055",
    "nomePais": "BRASIL",
    "agencias": [
      {
        "aeroporto": true,
        "codigoLocaliza": "AABHZ",
        "nome": "AGENCIA AEROPORTO CONFINS",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.6610851,
        "longitude": -43.9358526,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "ROD. LMG 800, KM 3, GOIABEIRAS",
            "cep": "33400-000",
            "uf": "MG",
            "cidade": "CONFINS",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "ROD. LMG 800, KM 3, GOIABEIRAS",
            "cep": "33400-000",
            "uf": "MG",
            "cidade": "CONFINS",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Domingo",
            "aberto24Horas": true
          },
          {
            "diaSemana": "Feriado",
            "aberto24Horas": true
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "00:00",
                  "fim": "23:59"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACCTA",
        "nome": "AG C BELO HORIZONTE - CATALAO",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.9043985,
        "longitude": -43.9653931,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV PRESIDENTE CARLOS LUZ, 561, CAICARAS",
            "cep": "31230-000",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV PRESIDENTE CARLOS LUZ, 561, CAICARAS",
            "cep": "31230-000",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "20:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado e Domingo",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Feriado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "20:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACBHO",
        "nome": "AG CENTRO BARAO HOMEM DE MELO",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.9463189,
        "longitude": -43.9696566,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV BARAO HOMEM DE MELO, 1280, JARDIM AMERICA",
            "cep": "30421-450",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV BARAO HOMEM DE MELO, 1280, JARDIM AMERICA",
            "cep": "30421-450",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACMAD",
        "nome": "AG CENTRO CRISTIANO MACHADO",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.880943,
        "longitude": -43.930125,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV CRISTIANO MACHADO, 2875, IPIRANGA",
            "cep": "31160-413",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV CRISTIANO MACHADO, 2875, IPIRANGA",
            "cep": "31160-413",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                },
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACBRO",
        "nome": "AGENCIA CENTRO BARREIRO",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.97494,
        "longitude": -44.01296,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV OLINTO MEIRELES, 440, BARREIRO",
            "cep": "30640-010",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV OLINTO MEIRELES, 440, BARREIRO",
            "cep": "30640-010",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACBHZ",
        "nome": "AGENCIA CENTRO BELO HORIZONTE",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.93256,
        "longitude": -43.92975,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV BERNARDO MONTEIRO, 1567, FUNCIONARIOS",
            "cep": "30150-281",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV BERNARDO MONTEIRO, 1567, FUNCIONARIOS",
            "cep": "30150-281",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sábado",
            "horarios": [
              {
                "inicio": "07:00",
                "fim": "20:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Domingo",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Feriado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "07:00",
                  "fim": "20:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACPAM",
        "nome": "AGENCIA CENTRO PAMPULHA",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.8543712,
        "longitude": -43.9573741,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV PROFESSOR MAGALHAES PENIDO, 269, PAMPULHA",
            "cep": "31270-700",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV PROFESSOR MAGALHAES PENIDO, 269, PAMPULHA",
            "cep": "31270-700",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "20:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Domingo",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Feriado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "20:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACRAJ",
        "nome": "AGENCIA CENTRO RAJA GABAGLIA",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.9670893,
        "longitude": -43.954182,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV RAJA GABAGLIA, 3950, ESTORIL",
            "cep": "30494-310",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV RAJA GABAGLIA, 3950, ESTORIL",
            "cep": "30494-310",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "12:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "12:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACSAV",
        "nome": "AGENCIA CENTRO SAVASSI",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.9403579,
        "longitude": -43.9333141,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "R RIO GRANDE DO NORTE, 1615, SAVASSI",
            "cep": "30130-138",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "R RIO GRANDE DO NORTE, 1615, SAVASSI",
            "cep": "30130-138",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "20:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Domingo",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Feriado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "14:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "20:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACVEX",
        "nome": "AGENCIA CENTRO VIA EXPRESSA",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.92318,
        "longitude": -43.97917,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV TEREZA CRISTINA, 2850, PADRE EUSTAQUIO",
            "cep": "30720-230",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV TEREZA CRISTINA, 2850, PADRE EUSTAQUIO",
            "cep": "30720-230",
            "uf": "MG",
            "cidade": "BELO HORIZONTE",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACBTI",
        "nome": "AGENCIA CENTRO BETIM",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.96393,
        "longitude": -44.18876,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV BANDEIRANTES, 450, VILA RECREIO",
            "cep": "32650-370",
            "uf": "MG",
            "cidade": "BETIM",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV BANDEIRANTES, 450, VILA RECREIO",
            "cep": "32650-370",
            "uf": "MG",
            "cidade": "BETIM",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACCON",
        "nome": "AGENCIA CENTRO CONTAGEM",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.94315,
        "longitude": -44.03672,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "AV JOAO CESAR DE OLIVEIRA, 881, ELDORADO",
            "cep": "32315-000",
            "uf": "MG",
            "cidade": "CONTAGEM",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "AV JOAO CESAR DE OLIVEIRA, 881, ELDORADO",
            "cep": "32315-000",
            "uf": "MG",
            "cidade": "CONTAGEM",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      },
      {
        "aeroporto": false,
        "codigoLocaliza": "ACNLA",
        "nome": "AGENCIA CENTRO NOVA LIMA",
        "telefone": "08009792020",
        "whatsapp": "08009792020",
        "latitude": -19.98331,
        "longitude": -43.94608,
        "enderecos": [
          {
            "tipo": "Retirada",
            "logadouro": "R DA PAISAGEM, 45, VILA DA SERRA",
            "cep": "34000-000",
            "uf": "MG",
            "cidade": "NOVA LIMA",
            "codigoPais": "0055"
          },
          {
            "tipo": "Devolucao",
            "logadouro": "R DA PAISAGEM, 45, VILA DA SERRA",
            "cep": "34000-000",
            "uf": "MG",
            "cidade": "NOVA LIMA",
            "codigoPais": "0055"
          }
        ],
        "resumoHorariosFuncionamento": [
          {
            "diaSemana": "Segunda a Sexta",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "18:00"
              }
            ],
            "aberto24Horas": false
          },
          {
            "diaSemana": "Sábado",
            "horarios": [
              {
                "inicio": "08:00",
                "fim": "12:00"
              }
            ],
            "aberto24Horas": false
          }
        ],
        "resumoExcecoesFuncionamento": {
          "abreNessesDias": [
            {
              "dia": "2021-02-16T00:00:00",
              "horarios": [
                {
                  "inicio": "08:00",
                  "fim": "18:00"
                }
              ]
            }
          ]
        },
        "agenciaParceiro": false,
        "agenciaFranqueado": false,
        "brasileira": true
      }
    ]
  }
]`)[0].agencias;

let response = await fetch('http://localhost:5000/api/session', {
    method: 'POST',
    headers: new Headers({
        "Content-Type": "application/json",
    }),
    body: JSON.stringify({
        document: '33333333399',
        password: 'grupo3',
    })
});
let json = await response.json();
let token = json.token;
let headers = new Headers({
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json",
});
let baseNumber = 1150;

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}

async function getModelId(model) {
    let modelResponse = await fetch('http://localhost:5000/api/vehiclemodel?name=' + model, {
        method: 'GET',
        headers: headers
    });
    let modelJson = await modelResponse.json();
    if (modelJson.length > 0) return modelJson[0].id;

    modelResponse = await fetch('http://localhost:5000/api/vehiclemodel', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify({
            name: model,
        })
    });
    modelJson = await modelResponse.json();
    return modelJson.id;
}

async function getBrandId(brand) {
    let brandResponse = await fetch('http://localhost:5000/api/vehiclebrand?name=' + brand, {
        method: 'GET',
        headers: headers
    });
    let brandJson = await brandResponse.json();
    if (brandJson.length > 0) return brandJson[0].id;

    brandResponse = await fetch('http://localhost:5000/api/vehiclebrand', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify({
            name: brand,
        })
    });
    brandJson = await brandResponse.json();
    return brandJson.id;
}

for (let agencia of agencias) {
    console.log(`Iniciando processo para agência ${agencia.nome}`);
    try {
        let agencyResponse = await fetch('http://localhost:5000/api/agency', {
            method: 'POST',
            headers: headers,
            body: JSON.stringify({
                name: agencia.nome,
                phone: agencia.telefone,
                whatsapp: agencia.whatsapp,
                latitude: agencia.latitude,
                longitude: agencia.longitude,
            })
        });
        let agencyJson = await agencyResponse.json();

        let localizaAgencyResponse = await fetch('https://canaisdigitais-api.localizahertz.com/canaisdigitais/Cotacoes/melhoresofertas', {
            method: 'POST',
            headers: new Headers({
                "Content-Type": "application/json",
            }),
            body: JSON.stringify({
                "codigoAgenciaRetirada":agencia.codigoLocaliza,
                "dataHoraRetirada":"2021-02-18 10:00",
                "reservaParceiro":false,
                "codigoAgenciaDevolucao":agencia.codigoLocaliza,
                "dataHoraDevolucao":"2021-02-26 10:00",
                "codigoPromocional":null,
                "utilizarPontos":false
            })
        });
        let localizaAgencyJson = await localizaAgencyResponse.json();

        for (let oferta of localizaAgencyJson.ofertas) {
            let veiculos = oferta.grupoVeiculo.veiculos;
            for (let veiculo of veiculos) {
                try {
                    let spaceIndex = veiculo.nome.indexOf(' ');
                    let marca = veiculo.nome.substr(0, spaceIndex);
                    let modelo = veiculo.nome.substr(spaceIndex);

                    let image = veiculo.urlImagem;
                    let fuelType = getRandomInt(0, 3);
                    await fetch('http://localhost:5000/api/vehicle', {
                        method: 'POST',
                        headers,
                        body: JSON.stringify({
                            plateLicense: `ABC-${baseNumber++}`,
                            image,
                            year: 2020,
                            fuelType,
                            hourlyPrice: 10,
                            trunkCapacity: 235,
                            tankCapacity: 47,
                            vehicleModelId: await getModelId(modelo),
                            vehicleBrandId: await getBrandId(marca),
                            vehicleCategoryId: [
                                'ce26d49d-a7f5-47c0-b9b7-08d8d39a4aef',
                                '9769a6c5-3918-4766-b9b8-08d8d39a4aef',
                                'f7dcfb6e-219f-4392-b9b9-08d8d39a4aef',
                            ][getRandomInt(0, 3)],
                            agencyId: agencyJson.id,
                        })
                    });
                } catch (e) {
                    console.log(`[ERRO] Veículo ${veiculo.nome}, agência ${agencia.codigoLocaliza}`)
                }
            }
        }
    } catch (e) {
        console.log(`[ERRO] agência: ${JSON.stringify(agencia)}`);
    }
}
