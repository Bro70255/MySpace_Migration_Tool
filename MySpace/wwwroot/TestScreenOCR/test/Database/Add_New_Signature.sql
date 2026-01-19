CREATE PROCEDURE [dbo].[Add_New_Signature]
(
    @created_by int,
    @signature varchar(100),
    @signatureName varchar(100),
    @unitName int,  -- Assuming UNIT is of type int
    @bankName varchar(100),
    @accountnum varchar(100)
)
AS 
BEGIN 
    SET NOCOUNT ON;

    -- Check if the record for the specific unit and bank exists
    IF NOT EXISTS (
        SELECT 1 
        FROM [dbo].[Bank_Signatures_Dtls]
        WHERE [UNIT] = @unitName AND [BANK] = @bankName
    )
    BEGIN
        -- Insert a new record with the provided details, including Created_by and Created_datetime
        INSERT INTO [dbo].[Bank_Signatures_Dtls] 
        (
            [UNIT], 
            [BANK], 
            [ACCOUNT_NUMBER], 
            [SIGNATURE1], 
            [SIGNATURE2], 
            [SIGNATURE3], 
            [SIGNATURE4], 
            [Created_by],  -- Insert Created_by
            [Created_datetime]  -- Insert Created_datetime
        )
        VALUES 
        (
            @unitName, 
            @bankName, 
            @accountnum, 
            CASE WHEN @signature = 'Signature1' THEN @signatureName ELSE NULL END