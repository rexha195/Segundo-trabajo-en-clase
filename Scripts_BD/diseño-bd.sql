-- Archivo: diseño-bd.sql

-- 1. Tabla de Automóviles
CREATE TABLE PRQ_Automoviles (
    id SERIAL PRIMARY KEY,
    color VARCHAR(50) NOT NULL,
    ano INT NOT NULL,
    fabricante VARCHAR(100) NOT NULL,
    tipo VARCHAR(20) NOT NULL CHECK (tipo IN ('sedán', '4x4', 'moto'))
);

-- 2. Tabla de Parqueo
CREATE TABLE PRQ_Parqueo (
    id SERIAL PRIMARY KEY,
    nombre_provincia VARCHAR(100) NOT NULL,
    nombre VARCHAR(150) NOT NULL,
    precio_por_hora DECIMAL(10, 2) NOT NULL
);

-- 3. Tabla de Ingreso de automóviles
CREATE TABLE PRQ_IngresoAutomoviles (
    consecutivo SERIAL PRIMARY KEY,
    id_parqueo INT NOT NULL,
    id_automovil INT NOT NULL,
    fecha_hora_entrada TIMESTAMP NOT NULL,
    fecha_hora_salida TIMESTAMP, -- Puede ser nulo cuando el auto no ha salido
    CONSTRAINT fk_parqueo FOREIGN KEY (id_parqueo) REFERENCES PRQ_Parqueo(id) ON DELETE CASCADE,
    CONSTRAINT fk_automovil FOREIGN KEY (id_automovil) REFERENCES PRQ_Automoviles(id) ON DELETE CASCADE
);