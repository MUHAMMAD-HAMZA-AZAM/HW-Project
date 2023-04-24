USE [Job]
GO
/****** Object:  StoredProcedure [dbo].[SP_ActiveBidsWeb]    Script Date: 25/02/2020 10:42:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ActiveBidsWeb]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_ActiveBidsWeb] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_ActiveBidsWeb]
@PageSize INT,
@pageNumber INT,
@tradesmanId INT,
@bidsStatusId INT
AS
BEGIN
SELECT DISTINCT  Bid.JobQuotationId JobQuotationid,Bid.Amount Budget,Bid.CreatedOn [Date],
jobQuotation.WorkTitle WorkTitle,jobImage.BidImage,
customerDetails.FirstName CustomerName,customerDetails.LastName
 FROM Job.[dbo].Bids Bid
INNER JOIN Job.[dbo].JobQuotation jobQuotation ON Bid.JobQuotationId=jobQuotation.JobQuotationId
LEFT OUTER JOIN Image.[dbo].JobImages jobImage ON Bid.JobQuotationId=jobImage.JobQuotationId AND jobImage.IsMain = 1
INNER JOIN Customer.[dbo].Customer customerDetails On Bid.CustomerId=customerDetails.CustomerId
WHERE Bid.TradesmanId=@tradesmanId AND Bid.StatusId=@bidsStatusId 
ORDER by Bid.CreatedOn DESC
OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
END



/****** Object:  StoredProcedure [dbo].[SP_DeclinedBidsWeb]    Script Date: 25/02/2020 10:44:53 ******/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeclinedBidsWeb]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_DeclinedBidsWeb] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_DeclinedBidsWeb]
@tradesmanId INT,
@bidsStatusId INT,
@pageNumber INT,
@PageSize INT
AS
BEGIN
SELECT DISTINCT  Bid.JobQuotationId JobQuotationid,Bid.Amount Budget,Bid.CreatedOn [Date],jobQuotation.WorkTitle WorkTitle,jobImage.BidImage,
customerDetails.FirstName CustomerName,customerDetails.LastName,city.[Name] WorkAddress,jobAddress.Area,jobAddress.StreetAddress 
 FROM Job.[dbo].Bids Bid
INNER JOIN Job.[dbo].JobQuotation jobQuotation ON Bid.JobQuotationId=jobQuotation.JobQuotationId
LEFT OUTER JOIN Image.[dbo].JobImages jobImage ON Bid.JobQuotationId=jobImage.JobQuotationId AND jobImage.IsMain = 1
INNER JOIN Customer.[dbo].Customer customerDetails On Bid.CustomerId=customerDetails.CustomerId
INNER JOIN JobAddress jobAddress On Bid.JobQuotationId=jobAddress.JobQuotationId
INNER JOIN UserManagement.[dbo].City city On jobAddress.CityId=city.CityId
WHERE Bid.TradesmanId=@tradesmanId AND Bid.StatusId=@bidsStatusId 
ORDER by Bid.CreatedOn DESC
OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
END

/****** Object:  StoredProcedure [dbo].[sp_JobQuotation_Web]    Script Date: 25/02/2020 10:46:11 ******/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_JobQuotation_Web]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_JobQuotation_Web] AS' 
END
GO
ALTER PROCEDURE [dbo].[sp_JobQuotation_Web]
@pageSize INT,
@pageNumber INT,
@tradesmanId BIGINT,
@StatusId INT
AS
BEGIN
select 
	DISTINCT (JobQuotation.JobQuotationId) JobQuotesId,COUNT(bids.JobQuotationId) BidCount,
	 JobQuotation.WorkTitle JobTitle, JobQuotation.WorkBudget Budget,JobQuotation.CreatedOn PostedOn, 
	images.[BidImage],jobAddress.Area Address,cities.Name City

from Job.[dbo].JobQuotation
LEFT OUTER JOIN  Job.[dbo].Bids bids ON JobQuotation.JobQuotationId = bids.JobQuotationId
LEFT OUTER JOIN [Image].[dbo].[JobImages] images ON JobQuotation.JobQuotationId = images.JobQuotationId AND images.IsMain = 1
INNER JOIN Job.dbo.JobAddress jobAddress ON JobQuotation.JobQuotationId = jobAddress.JobQuotationId
INNER JOIN Tradesman.[dbo].SkillSet skillSet ON JobQuotation.SkillId=skillSet.SkillId 
INNER JOIN Tradesman.[dbo].Tradesman tradesman ON skillSet.TradesmanId = tradesman.TradesmanId
INNER JOIN UserManagement.[dbo].City cities ON tradesman.City = cities.[Name]
INNER JOIN Job.[dbo].JobAddress _jobAddress ON JobQuotation.JobQuotationId=_jobAddress.JobQuotationId
where JobQuotation.StatusId=@StatusId and tradesman.TradesmanId = @tradesmanId AND ISNULL(bids.TradesmanId, 0) != @tradesmanId  
AND _jobAddress.CityId=cities.CityId
and isnull((select COUNT(JobQuotationId)  from Job.[dbo].Bids where JobQuotationId = JobQuotation.JobQuotationId group by JobQuotationId), 0) <= JobQuotation.DesiredBids
 GROUP BY bids.JobQuotationId, JobQuotation.JobQuotationId,
 JobQuotation.JobQuotationId, JobQuotation.WorkTitle, JobQuotation.WorkBudget,JobQuotation.CreatedOn, 
	images.BidImage,jobAddress.Area,cities.Name 
	ORDER BY JobQuotation.CreatedOn Desc
OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
END

/****** Object:  StoredProcedure [dbo].[SP_myJobs_Web]    Script Date: 25/02/2020 10:49:39 ******/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_myJobs_Web]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_myJobs_Web] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_myJobs_Web]
@tradesmanId INT,
@jobStatusId INT,
@pageNumber INT,
@PageSize INT
AS
BEGIN
if(@jobStatusId=1)
SELECT DISTINCT jobDetails.JobQuotationId JobQuotationId,jobDetails.JobDetailId JobDetailId,jobDetails.CreatedOn [Date],jobDetails.Title WorkTitle,jobImage.BidImage,Customer.FirstName CustomerName,Customer.LastName
 FROM Job.[dbo].JobDetail jobDetails
 INNER JOIN Job.[dbo].JobQuotation jobQuotation ON jobDetails.JobQuotationId=jobQuotation.JobQuotationId
 LEFT OUTER JOIN Image.[dbo].JobImages jobImage On jobDetails.JobQuotationId=jobImage.JobQuotationId AND jobImage.IsMain=1
 INNER JOIN Customer.[dbo].Customer Customer ON jobDetails.CustomerId=Customer.CustomerId
 WHERE jobDetails.StatusId=@jobStatusId AND jobDetails.TradesmanId=@tradesmanId
ORDER BY jobDetails.CreatedOn DESC 
OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

else
SELECT DISTINCT jobDetails.JobQuotationId JobQuotationId,jobDetails.JobDetailId JobDetailId,jobDetails.CreatedOn [Date],jobDetails.Title WorkTitle,jobImage.BidImage,Customer.FirstName CustomerName,Customer.LastName,tradesmanFeedback.OverallRating Rating
 FROM Job.[dbo].JobDetail jobDetails
 INNER JOIN Job.[dbo].JobQuotation jobQuotation ON jobDetails.JobQuotationId=jobQuotation.JobQuotationId
 LEFT OUTER JOIN Image.[dbo].JobImages jobImage On jobDetails.JobQuotationId=jobImage.JobQuotationId AND jobImage.IsMain=1
 LEFT OUTER JOIN Job.[dbo].TradesmanFeedback tradesmanFeedback ON tradesmanFeedback.TradesmanId=@tradesmanId AND tradesmanFeedback.JobDetailId=jobDetails.JobDetailId	
 INNER JOIN Customer.[dbo].Customer Customer ON jobDetails.CustomerId=Customer.CustomerId
 WHERE jobDetails.StatusId=@jobStatusId AND jobDetails.TradesmanId=@tradesmanId
ORDER BY jobDetails.CreatedOn DESC 
OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
END


/****** Object:  StoredProcedure [dbo].[WebLiveLeads]    Script Date: 25/02/2020 10:56:55 ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebLiveLeads]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[WebLiveLeads] AS' 
END
GO
ALTER PROCEDURE [dbo].[WebLiveLeads] @jobQuotationId BIGINT 
AS
BEGIN 
IF @jobQuotationId > 0
BEGIN
SELECT JobQuotation.worktitle WorkTitle, JobQuotation.workBudget WorkBudget, JobQuotation.jobQuotationId JobQuotationId,
jobimages.BidImage JobImage,
JobQuotation.createdOn CreatedOn, jobAddress.Area Area, City.Name CityName FROM Job.[dbo].jobQuotation JobQuotation
INNER JOIN Job.[dbo].jobaddress jobaddress ON JobQuotation.jobQuotationId = jobaddress.jobQuotationId
INNER JOIN usermanagement.[dbo].City City ON jobaddress.CityId = City.CityId
LEFT JOIN [Image].[dbo].jobImages jobimages ON JobQuotation.jobQuotationId = jobimages.jobQuotationId AND jobimages.ismain = 1 
WHERE JobQuotation.JobQuotationId=@jobQuotationId
END
ELSE
BEGIN 
SELECT TOP 20 JobQuotation.worktitle WorkTitle, JobQuotation.workBudget WorkBudget, JobQuotation.jobQuotationId JobQuotationId, jobimages.BidImage JobImage,
JobQuotation.createdOn CreatedOn, jobAddress.Area Area, City.Name CityName FROM Job.[dbo].jobQuotation JobQuotation
INNER JOIN Job.[dbo].jobaddress jobaddress ON JobQuotation.jobQuotationId = jobaddress.jobQuotationId
INNER JOIN usermanagement.[dbo].City City ON jobaddress.CityId = City.CityId
LEFT OUTER JOIN [Image].[dbo].jobImages jobimages ON JobQuotation.jobQuotationId = jobimages.jobQuotationId AND jobimages.ismain = 1
ORDER BY JobQuotation.CreatedOn DESC
END
End
GO


USE [Supplier]
GO
/****** Object:  StoredProcedure [dbo].[SP_ActiveAds_Web]    Script Date: 25/02/2020 10:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ActiveAds_Web]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_ActiveAds_Web] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_ActiveAds_Web]
@PageSize INT,
@pageNumber INT,
@supplierId INT
AS
BEGIN

SELECT DISTINCT SupplierAds.SupplierAdsId SupplierAdsId,SupplierAds.AdTitle AdTitle,supplierAds.Price Price,supplierAds.AdsStatusId,
SupplierAds.ActiveFrom ActiveFrom,SupplierAds.ActiveTo ActiveTo,
SupplierAds.AdViewCount AdViewCount,SupplierAds.Town Town,SupplierAds.[Address] Addres,SupplierAdImage.AdImage AdImage, 
ProductSubCategory.[SubCategoryName] SubCategoryValue, city.[Name] City,supplierads.createdOn
FROM supplier.[dbo].SupplierAds SupplierAds
INNER JOIN Supplier.[dbo].Supplier supplier ON supplierads.SupplierId=supplier.supplierId
INNER JOIN UserManagement.[dbo].City city ON supplierads.CityId = city.cityId
INNER JOIN supplier.[dbo].productsubcategory productsubcategory ON supplierAds.productSubCategoryId= productsubcategory.ProductSubCategoryId 
LEFT OUTER JOIN Image.[dbo].SupplierAdImage supplierAdImage ON
SupplierAds.supplierAdsId=supplierAdImage.supplieradsId AND supplieradImage.IsMain = 1
WHERE supplierAds.supplierId=@supplierId AND supplierAds.activeTo > GETDATE()
ORDER by supplierAds.supplierAdsId DESC
OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;
END



/****** Object:  StoredProcedure [dbo].[SP_InActiveAds_Web]    Script Date: 25/02/2020 10:54:35 ******/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_InActiveAds_Web]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_InActiveAds_Web] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_InActiveAds_Web]
@PageSize INT,
@pageNumber INT,
@supplierId INT
AS
BEGIN
SELECT DISTINCT  SupplierAds.SupplierAdsId SupplierAdsId,SupplierAds.AdTitle AdTitle,supplierAds.Price Price,supplierAds.AdsStatusId,
SupplierAds.ActiveFrom ActiveFrom,SupplierAds.ActiveTo ActiveTo,
SupplierAds.AdViewCount AdViewCount,SupplierAds.Town Town,SupplierAds.[Address] Addres,SupplierAdImage.AdImage AdImage, 
ProductSubCategory.[SubCategoryName] SubCategoryValue, city.[Name] City,supplierads.createdOn

FROM supplier.[dbo].SupplierAds SupplierAds
INNER JOIN Supplier.[dbo].Supplier supplier ON supplierads.SupplierId=supplier.supplierId
INNER JOIN UserManagement.[dbo].City city ON supplierads.CityId = city.cityId
INNER JOIN supplier.[dbo].productsubcategory productsubcategory ON supplierAds.productSubCategoryId= productsubcategory.ProductSubCategoryId 
LEFT OUTER JOIN Image.[dbo].SupplierAdImage supplierAdImage ON
SupplierAds.supplierAdsId=supplierAdImage.supplieradsId AND supplieradImage.IsMain = 1

WHERE supplierAds.supplierId=@supplierId AND supplierAds.activeTo < GETDATE()
ORDER by supplierAds.supplierAdsId DESC

OFFSET (@pageNumber - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

END