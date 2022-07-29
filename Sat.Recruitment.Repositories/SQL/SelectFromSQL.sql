
DECLARE @IDMonedaDefecto int
SET @IDMonedaDefecto = (SELECT IDMoneda FROM Monedas WHERE IDUsuario = @IDUsuario AND PorDefecto = 1)

SELECT 
	vd.IDDetalle AS IDDetalle, 
	v.IDComprobante AS IDComprobante, 
	con.IDConcepto AS IDConcepto,
	v.Tipo AS Tipo, 
	vd.Cantidad AS Cantidad,
	case when con.IDMoneda = v.IDMoneda 
		then vd.PrecioUnitario
		else case when v.IDMoneda = @IDMonedaDefecto
				then vd.PrecioUnitario / v.TipoDeCambio
				else vd.PrecioUnitario * v.TipoDeCambio
				end
		end AS PrecioUnitario, 
	vd.Bonificacion AS Bonificacion,
	con.Tipo AS ConceptoTipo, 
	con.Nombre AS ConceptoNombre, 
	vd.Iva AS ConceptoIVA, 
	con.Codigo AS ConceptoCodigo, 
	mb.CodigoMoneda AS CodigoMoneda, 
	con.CostoInterno AS ConceptoCosto,
	v.FechaComprobante AS FechaComprobante
FROM ComprobantesDetalle vd (nolock)
	INNER JOIN Comprobantes v (nolock) ON v.IDComprobante = vd.IDComprobante
	INNER JOIN Conceptos con (nolock) ON con.IDConcepto = vd.IDConcepto
	INNER JOIN Monedas m (nolock) on m.IDMoneda = con.IDMoneda
	INNER JOIN MonedasBase mb (nolock) on mb.IDMonedaBase = m.IDMonedaBase
WHERE 
	(v.FechaComprobante BETWEEN @fechaDesde AND @fechaHasta) AND 
	--con.IDConcepto = 868307 AND
	con.IDUsuario = @idUsuario AND 
	v.IDUsuario = @idUsuario AND 
	(@idConcepto = 0 OR con.IDConcepto = @idConcepto) AND 
	con.Tipo IN ('P', 'C') AND 
	vd.Cantidad > 0 AND 
	vd.PrecioUnitario > 0 


