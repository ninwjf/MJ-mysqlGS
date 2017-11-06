
---------------------------------五。初始化数据Begin---------------------------------
--用户表 默认123456
INSERT INTO UserInfo(UserName,UserPwd,Flag,IfFrezen) VALUES('admin','e10adc3949ba59abbe56e057f20f883e',0,0)
INSERT INTO UserInfo(UserName,UserPwd,Flag,IfFrezen) VALUES('test','e10adc3949ba59abbe56e057f20f883e',1,0)

--建筑表
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('默认小区',0,1,0)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1',1,1,1)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋2',1,2,1)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋3',1,3,1)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋4',1,4,1)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋5',1,5,1)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1',2,1,2)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元2',2,2,2)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元3',2,3,2)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层1',3,1,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层2',3,2,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层3',3,3,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层4',3,4,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层5',3,5,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层6',3,6,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层7',3,7,7)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层1房间1',4,1,10)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层1房间2',4,2,10)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层1房间3',4,3,10)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层1房间4',4,4,10)
INSERT INTO Building(Bname,Flag,Code,FID) VALUES('楼栋1单元1楼层1房间5',4,5,10)

--卡片表
INSERT INTO Card(BuildingID,RAreaCode,RBuildCode,RUnitCode,RFloorCode,RRoomCode,CardNo,CardType,SerialNo,ExpiryDate,IfFrezen,CreateDate) VALUES(17,0,0,0,0,0,'1234578',0,'00112233445566778899aabbccddeeff',100000,0,10000)
INSERT INTO Card(BuildingID,RAreaCode,RBuildCode,RUnitCode,RFloorCode,RRoomCode,CardNo,CardType,SerialNo,ExpiryDate,IfFrezen,CreateDate) VALUES(0,0,0,0,0,0,'1234579',3,'00112233445566778899aabbccddeeff',100000,0,10000)
INSERT INTO CardLog(RAreaCode,RBuildCode,RUnitCode,RFloorCode,RRoomCode,CardNo,CardType,SerialNo,DeviceType,DeviceNo,Contact,Tel,Memo,CreateDate) VALUES(0,0,0,0,0,'1234578',0,'00112233445566778899aabbccddeeff',1,'设备编号','联系人','电话','备注',10000)


---------------------------------五。初始化数据End---------------------------------