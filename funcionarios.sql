--O cadastro deve conter:
--Dados pessoais
--Dados completos da Carteira de Trabalho (CTPS)
--Dados da CNH (opcional)
--1 ou mais cursos realizados (mínimo 1 obrigatório)
--1 ou mais endereços (mínimo 1 obrigatório)
--Profissão (campo obrigatório)

--O sistema deve aceitar 0 ou 1 CNH por funcionário (opcional). 
--O sistema deve aceitar 1 ou mais cursos cadastrados para cada funcionário. 
--O sistema deve aceitar 1 ou mais endereços cadastrados para cada funcionário.

--validar dados obrigatórios: 
--CPF válido 
--Datas coerentes (ex.: validade da CNH ≥ data atual) 
--CEP no formato brasileiro 
--UF com 2 letras

---Não é permitido cadastrar funcionário sem CTPS. 
--CNH é opcional, mas se cadastrada deve conter número, categoria, UF e validade.

CREATE TABLE profissoes (
    id SERIAL PRIMARY KEY, 
    nome VARCHAR(100) NOT NULL UNIQUE,
    descricao TEXT,
    ativo BOOLEAN DEFAULT TRUE,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE funcionarios (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    cpf VARCHAR(11) NOT NULL UNIQUE,
    rg VARCHAR(15),
    dtnasc DATE NOT NULL,
    telefone VARCHAR(15),
    celular VARCHAR(15),
    email VARCHAR(150),
    profissao_id INTEGER NOT NULL,
    ativo BOOLEAN DEFAULT TRUE,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE funcionarios 
ADD FOREIGN KEY (profissao_id) REFERENCES profissoes(id);

ALTER TABLE funcionarios 
ADD CHECK (cpf ~ '^[0-9]{11}$');

ALTER TABLE funcionarios 
ADD CHECK (dtnasc <= CURRENT_DATE);

ALTER TABLE funcionarios
ADD CHECK (email IS NULL OR email ~ '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$');


CREATE TABLE ctps (
    id SERIAL PRIMARY KEY,
    funcionario_id INTEGER NOT NULL UNIQUE,
    numero VARCHAR(20) NOT NULL,
    serie VARCHAR(10) NOT NULL,
    uf_emissao CHAR(2) NOT NULL,
    dtemissao DATE NOT NULL,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE ctps
ADD FOREIGN KEY (funcionario_id) REFERENCES funcionarios(id) ON DELETE CASCADE;

ALTER TABLE ctps
ADD CHECK (uf_emissao ~ '^[A-Z]{2}$');

ALTER TABLE ctps
ADD CHECK (dtemissao <= CURRENT_DATE);


CREATE TABLE cnh (
    id SERIAL PRIMARY KEY,
    funcionario_id INTEGER NOT NULL UNIQUE,
    numero VARCHAR(15) NOT NULL UNIQUE,
    categoria VARCHAR(5) NOT NULL,
    uf_emissao CHAR(2) NOT NULL,
    dtvalidade DATE NOT NULL,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE cnh
ADD FOREIGN KEY (funcionario_id) REFERENCES funcionarios(id) ON DELETE CASCADE;

ALTER TABLE cnh
ADD CHECK (categoria IN ('A', 'B', 'AB', 'C', 'D', 'E', 'AC', 'AD', 'AE'));

ALTER TABLE cnh
ADD CHECK (uf_emissao ~ '^[A-Z]{2}$');

ALTER TABLE cnh
ADD CHECK (dtvalidade >= CURRENT_DATE);



CREATE TABLE cursos (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(150) NOT NULL,
    instituicao VARCHAR(150),
    carga_horaria INTEGER,
    descricao TEXT,
    ativo BOOLEAN DEFAULT TRUE,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE cursos
ADD CHECK (carga_horaria IS NULL OR carga_horaria > 0);


CREATE TABLE funcionario_cursos (
    id SERIAL PRIMARY KEY,
    funcionario_id INTEGER NOT NULL,
    curso_id INTEGER NOT NULL,
    dtconclusao DATE,
    certificado_numero VARCHAR(50),
    observacoes TEXT,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE funcionario_cursos
ADD FOREIGN KEY (funcionario_id) REFERENCES funcionarios(id) ON DELETE CASCADE;

ALTER TABLE funcionario_cursos
ADD FOREIGN KEY (curso_id) REFERENCES cursos(id);

ALTER TABLE funcionario_cursos
ADD UNIQUE (funcionario_id, curso_id);

ALTER TABLE funcionario_cursos
ADD CHECK (dtconclusao IS NULL OR dtconclusao <= CURRENT_DATE);



CREATE TABLE enderecos (
    id SERIAL PRIMARY KEY,
    funcionario_id INTEGER NOT NULL,
    tp_endereco VARCHAR(20) DEFAULT 'RESIDENCIAL',
    logradouro VARCHAR(200) NOT NULL,
    numero VARCHAR(10),
    complemento VARCHAR(100),
    bairro VARCHAR(100) NOT NULL,
    cidade VARCHAR(100) NOT NULL,
    uf CHAR(2) NOT NULL,
    cep VARCHAR(8) NOT NULL,
    principal BOOLEAN DEFAULT FALSE,
    ativo BOOLEAN DEFAULT TRUE,
    dtcriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    dtatualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE enderecos
ADD FOREIGN KEY (funcionario_id) REFERENCES funcionarios(id) ON DELETE CASCADE;

ALTER TABLE enderecos
ADD CHECK (tp_endereco IN ('RESIDENCIAL', 'COMERCIAL'));

ALTER TABLE enderecos
ADD CHECK (uf ~ '^[A-Z]{2}$');

ALTER TABLE enderecos
ADD CHECK (cep ~ '^[0-9]{8}$');
