select * from GD_HANG_HOA_TAG
delete from GD_HANG_HOA_TAG where ID_HANG_HOA > 3
DBCC CHECKIDENT ( GD_HANG_HOA_TAG, RESEED, 12)

select * from DM_LINK_ANH
delete from DM_LINK_ANH where ID_HANG_HOA > 3
DBCC CHECKIDENT (DM_LINK_ANH, RESEED, 4)

select * from GD_TAG
delete from GD_TAG where ID > 24
DBCC CHECKIDENT (GD_TAG, RESEED, 24)


select * from DM_HANG_HOA
delete from DM_HANG_HOA where ID > 3
DBCC CHECKIDENT (DM_HANG_HOA, RESEED, 3)