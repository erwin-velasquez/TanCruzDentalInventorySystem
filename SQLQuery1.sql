CREATE PROCEDURE [dbo].[GetSalesOrderPaymentDetail]
	@SalesOrderPaymentDetailId	CHAR (10)
AS
	SELECT
		sopDtls.SOPAY_LINE_ID		SalesOrderDetailId,
		sopDtls.SO_PAYMENT_ID		SalesOrderId,
		sopDtls.PAYMENT_TYPE							PaymentType,
		sopDtls.CHECK_NUMBER				CheckNumber,
		sopDtls.LINE_TOTAL			SalesOrderPaymentDetailTotal,
		sopDtls.REMARKS		Remarks,
		sopDtls.CHANGE_ID				UserId
	FROM T3_SO_PAYMENT_DETAILS sopDtls
	WHERE sopDtls.SOPAY_LINE_ID = @SalesOrderPaymentDetailId;

RETURN 0