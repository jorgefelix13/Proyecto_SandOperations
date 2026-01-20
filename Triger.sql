use SandOperations
go

CREATE TRIGGER TR_RestarStock
ON salidas
AFTER INSERT
AS
BEGIN
    -- Actualiza la tabla productos
    UPDATE p
    SET p.pro_stock = p.pro_stock - i.sal_cantidad
    FROM productos p
    INNER JOIN inserted i ON p.pro_id = i.sal_proId;
END
go

create TRIGGER TR_SumarStock
ON entradas
AFTER INSERT
AS
BEGIN
    -- 1. Actualiza el Stock (Suma cantidad)
    -- 2. Actualiza el Precio de Compra (Pone el nuevo precio de la entrada)
    UPDATE p
    SET 
        p.pro_stock = p.pro_stock + i.ent_cantidad,
        p.pro_pCompra = i.ent_precioUnitario -- <--- AQUÍ ACTUALIZAMOS EL PRECIO
    FROM productos p
    INNER JOIN inserted i ON p.pro_id = i.ent_proId;
END
go
