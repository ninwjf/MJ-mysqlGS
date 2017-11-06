#################################一。创建表结构Begin#################################
##用户表
 DROP Table IF EXISTS UserInfo;
##GO

CREATE TABLE UserInfo (
       ID   int AUTO_INCREMENT,
       UserName  varchar (50) NOT NULL,
       UserPwd  varchar (32) NOT NULL,
       Flag  int NOT NULL default 1,
       IfFrezen int NOT NULL default 0,
       Memo varchar (200) NULL,
       PRIMARY KEY (ID ASC))AUTO_INCREMENT=1;

CREATE INDEX UIndex_UserInfo_UserName ON UserInfo(UserName);
##GO

##建筑表
 DROP Table IF EXISTS Building;
##GO

CREATE TABLE Building (
       ID   int AUTO_INCREMENT,
       BName  varchar (50) NOT NULL,
       Flag  int NOT NULL,
       Code  int NOT NULL,
       FID  int NOT NULL,
       Contact varchar (50) NULL,
       Tel varchar (100) NULL,
       BuildingSerialNo varchar (50) NULL,
       PRIMARY KEY (ID ASC))AUTO_INCREMENT=1;

CREATE INDEX Uindex_Building_FID_Flag_Code ON Building(FID,Flag,Code);
##GO

##卡片表
 DROP Table IF EXISTS Card;
##GO

CREATE TABLE Card (
       ID   int AUTO_INCREMENT,
       BuildingID  int NOT NULL,
       CardNo bigint unsigned NOT NULL,
       RAreaCode  int NOT NULL,
       RBuildCode  int NOT NULL,
       RUnitCode  int NOT NULL,      
       RRoomCode  int NOT NULL,
       CardType  int NOT NULL default 0,
       SerialNo  varchar (50) NOT NULL,
       ExpiryDate  bigint NOT NULL,
       IfFrezen int NOT NULL default 0,
       CreateDate  bigint NOT NULL,
       Contact varchar (50) NULL,
       Tel varchar (100) NULL,
       Memo varchar (200) NULL,
       PRIMARY KEY (ID ASC))AUTO_INCREMENT=1;

CREATE INDEX Uindex_Card_CardNo ON Card(CardNo);
##GO

##刷卡日志表
 DROP Table IF EXISTS CardLog;
##GO

CREATE TABLE CardLog (
       ID   int AUTO_INCREMENT,
       CardNo  bigint unsigned NOT NULL,
       RAreaCode  int NOT NULL,
       RBuildCode  int NOT NULL,
       RUnitCode  int NOT NULL,     
       RRoomCode  int NOT NULL,
       CardType  int NOT NULL,
       DeviceType  int NOT NULL,
       DeviceNo  varchar (100) NULL,
       SerialNo  varchar (50) NULL,
       Contact varchar (50) NULL,
       Tel varchar (100) NULL,
       Memo varchar (200) NULL,
       CreateDate  varchar (11) NOT NULL,
       PRIMARY KEY (ID ASC))AUTO_INCREMENT=1;
##GO

##监控通讯日志表
 DROP Table IF EXISTS CommLog;
##GO

CREATE TABLE CommLog (
       ID   int AUTO_INCREMENT,
       Flag  int NOT NULL,
       Content  text NOT NULL,
       CreateDate  int NOT NULL,
       PRIMARY KEY (ID ASC))AUTO_INCREMENT=1;
##GO

#################################一。创建表结构End#################################





#################################二。创建视图Begin#################################
##用户视图
CREATE OR REPLACE VIEW V_UserInfo
AS
	SELECT *,CASE Flag WHEN 0 THEN '超级管理员' WHEN 1 THEN '普通管理员' ELSE '未知类型' END AS FlagDesc FROM UserInfo;

##GO

##所有建筑视图
CREATE OR REPLACE VIEW V_Building
AS
	SELECT *,CASE Flag WHEN 0 THEN '小区' WHEN 1 THEN '楼栋' WHEN 2 THEN '单元' WHEN 3 THEN '楼层' WHEN 4 THEN '房间' ELSE '未知类型' END AS FlagDesc FROM Building;

##GO

##小区视图
CREATE OR REPLACE VIEW V_Area
AS
	SELECT *,BuildingSerialNo As AreaSerialNo,'小区' as FlagDesc FROM Building WHERE Flag=0;

##GO

##栋视图
CREATE OR REPLACE VIEW V_Build
AS
	SELECT A.*,'楼栋' AS FlagDesc,B.ID AS AreaID,B.Bname AS AreaName,B.Code AS AreaCode,B.BuildingSerialNo As AreaSerialNo FROM Building A LEFT JOIN Building B ON A.FID=B.ID WHERE A.Flag=1;

##GO

##单元视图
CREATE OR REPLACE VIEW V_Unit
AS
	SELECT A.*,'单元' AS FlagDesc,B.ID AS BuildID,B.Bname AS BuildName,B.Code AS BuildCode,B.AreaID,B.AreaName,B.AreaCode,B.AreaSerialNo FROM Building A LEFT JOIN V_Build B ON A.FID=B.ID WHERE A.Flag=2;
##GO

##房间视图
CREATE OR REPLACE VIEW V_Room
AS
	SELECT A.*,'房间' AS FlagDesc,B.ID AS UnitID,B.Bname AS UnitName,B.Code AS UnitCode,B.BuildID,B.BuildName,B.BuildCode,B.AreaID,B.AreaName,B.AreaCode,B.AreaSerialNo FROM Building A LEFT JOIN V_Unit B ON A.FID=B.ID WHERE A.Flag=3;
##GO

##卡片视图
CREATE OR REPLACE VIEW V_Card
AS
    SELECT
A.*,
CASE A.CardType WHEN 1 THEN '巡更卡' WHEN 2 THEN '管理卡' WHEN 0 THEN '用户卡' ELSE '未知' END AS CardTypeDesc,
IFNULL(C.ID, 0) as RoomID,
IFNULL(C.BName, '') as RoomName,
IFNULL(C.Code, '-1') as RoomCode,
IFNULL(C.UnitID, 0) as UnitID,
IFNULL(C.UnitName, '') as UnitName,
IFNULL(C.UnitCode, '-1') as UnitCode,
IFNULL(C.BuildID, 0) as BuildID,
IFNULL(C.BuildName, '') as BuildName,
IFNULL(C.BuildCode, '-1') as BuildCode,
IFNULL(IFNULL(C.AreaID, B.ID), 0) as AreaID,
IFNULL(IFNULL(C.AreaName, B.BName), '') as AreaName,
IFNULL(IFNULL(C.AreaCode, B.Code), '-1') as AreaCode
FROM Card A
LEFT JOIN V_Room C  
ON A.BuildingID=C.ID
AND A.CardType = 0
LEFT JOIN V_AREA B  
ON A.BuildingID=B.ID 
AND A.CardType IN (1,2);
##GO

##刷卡日志视图
CREATE OR REPLACE VIEW V_CardLog
AS
	SELECT A.*,CASE A.CardType WHEN 0 THEN '用户卡' WHEN 1 THEN '巡更卡' WHEN 2 THEN '管理卡' WHEN 3 THEN '临时卡' WHEN 4 THEN '住户键盘密码输入' WHEN 5 THEN '公共键盘密码输入' WHEN 6 THEN '非注册卡' ELSE '未知' END AS CardTypeDesc
	,CASE A.DeviceType WHEN 1 THEN '管理机' WHEN 2 THEN '交换机' WHEN 3 THEN '切换器' WHEN 4 THEN '围墙刷卡头' WHEN 5 THEN '围墙机' WHEN 6 THEN '门口机' WHEN 7 THEN '二次门口机' ELSE '未知' END AS DeviceTypeDesc
	,B.ExpiryDate,B.RoomID,B.RoomName,B.RoomCode,B.UnitID,B.UnitName,B.UnitCode,B.BuildID,B.BuildName,B.BuildCode,B.AreaID,B.AreaName,B.AreaCode
	FROM CardLog A LEFT JOIN V_Card B ON A.CardNo=B.CardNo;
##GO

##监控通讯日志视图
CREATE OR REPLACE VIEW V_CommLog
AS
	SELECT *,CASE Flag WHEN 0 THEN '上行(接收)' WHEN 1 THEN '下行(发送)' ELSE '未知' END AS FlagDesc FROM CommLog;
##GO

#################################二。创建视图End#################################



#################################三。创建存储过程Begin#################################
##用户查询
 DROP PROCEDURE IF EXISTS  UserInfo_GetBywhere_Num;
##GO
CREATE PROCEDURE UserInfo_GetBywhere_Num(
SelNum int,
Where_String varchar(800)
)
BEGIN
	SET @strSQL = concat('SELECT * FROM V_UserInfo WHERE ', Where_String, ' LIMIT ', SelNum);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##用户新增或修改
 DROP PROCEDURE IF EXISTS  UserInfo_ADD_UPDATE;
##GO

CREATE PROCEDURE UserInfo_ADD_UPDATE(
OUT ErrorInfo varchar(500),
OUT NewID int,
Tag int,
iID int,
iUserName varchar(50),
iUserPwd varchar(32),
iFlag int,
iIfFrezen bit,
iMemo varchar(200)
)
BEGIN
	SET NewID = 0;
	SET ErrorInfo = '';
	IF(Tag = 0 OR Tag = 1)##正确的操作类型
	THEN
		IF(iFlag >= 0 AND iFlag <= 1)
		THEN
			IF(Tag = 0) ##新增
			THEN
				IF NOT EXISTS(SELECT * FROM UserInfo WHERE UserName=iUserName)
				THEN
					INSERT INTO UserInfo (UserName, UserPwd, Flag, IfFrezen, Memo) VALUES(iUserName, iUserPwd, iFlag, iIfFrezen, iMemo) ;
					SET NewID = LAST_INSERT_ID();
				ELSE
					SET ErrorInfo = concat('系统中已经存在名称为', iUserName, '的用户了');
				END IF;
			ELSE ##修改
         SELECT 1;
                ##如果存在用户名一样,ID不同的客户,说明更改的用户名被占用
				IF NOT EXISTS(SELECT * FROM UserInfo WHERE UserName=iUserName AND ID<>iID)
				THEN
              SELECT 2;
					UPDATE UserInfo SET UserName=iUserName,UserPwd=iUserPwd,Flag=iFlag,IfFrezen=iIfFrezen,Memo=iMemo WHERE ID=iID;
					SET NewID = iID;
				ELSE
              SELECT 3;
					SET ErrorInfo = concat('系统中已经存在名称为', iUserName, '的用户了');
                END IF;
            END IF;
		ELSE
			SET ErrorInfo = '非法类型';
        END IF;
	ELSE
		SET ErrorInfo = '非法操作';
    END IF;
END;
##GO

##用户删除
 DROP PROCEDURE IF EXISTS  UserInfo_DeleteByID;
##GO
CREATE PROCEDURE UserInfo_DeleteByID(
iID int
)
BEGIN
	DELETE FROM UserInfo WHERE ID=iID;
END;
##GO

##用户登录
 DROP PROCEDURE IF EXISTS  UserInfo_Login;
##GO

CREATE PROCEDURE UserInfo_Login(
OUT oID int,
iUserName varchar(50),
iUserPwd varchar(50)
)
BEGIN
	IF EXISTS(SELECT * FROM UserInfo WHERE UserName=iUserName AND UserPwd=iUserPwd)
	THEN
		SELECT ID INTO oID FROM UserInfo WHERE UserName=iUserName AND UserPwd=iUserPwd;
	ELSE
		SET oID = 0;
	END IF;
END;
##GO

##用户修改密码
 DROP PROCEDURE IF EXISTS  UserInfo_ChangePassword;
##GO

CREATE PROCEDURE UserInfo_ChangePassword(
OUT IfSucc bit,
OUT ErrorInfo varchar(500),
UserID int,
OldPasword varchar(50),
NewPasword varchar(50)
)
BEGIN
	DECLARE ResourcePasword varchar(50);  ## 旧密码
	IF EXISTS(SELECT * FROM UserInfo WHERE ID=UserID)
	THEN
		SELECT IFNULL(UserPwd,'') INTO ResourcePasword FROM UserInfo WHERE ID=UserID;
		IF(OldPasword = ResourcePasword)
		THEN
			UPDATE UserInfo SET UserPwd = NewPasword WHERE ID=UserID;
			SET IfSucc = TRUE;
		ELSE
			SET IfSucc = FALSE;
			SET ErrorInfo = '旧密码不正确';
		END IF;
	ELSE
		SET IfSucc = FALSE;
		SET ErrorInfo = concat('系统中不存在编号为', UserID, '的用户');
	END IF;
END;
##GO

##建筑查询
 DROP PROCEDURE IF EXISTS  Building_GetBywhere_Num;
##GO
CREATE PROCEDURE Building_GetBywhere_Num(
Flag int,##建筑类型(0：区；1：栋；2：单元；3：房间；)
SelNum int,
Where_String varchar(800) 
)
BEGIN
	DECLARE strViewName varchar(50) ;
	SET strViewName = 'V_Building';
	IF(Flag = 0)##区
    THEN
		SET strViewName = 'V_Area';
	ELSEIF(Flag = 1)##栋
	THEN
		SET strViewName = 'V_Build';
	ELSEIF(Flag = 2)##单元
	THEN
		SET strViewName = 'V_Unit';
	ELSEIF(Flag = 3)##房间
	THEN
		SET strViewName = 'V_Room';
	ELSE
		SET strViewName = 'V_Building';
	END IF;
	SET @strSQL = concat('SELECT * FROM ', strViewName, ' WHERE ', Where_String, ' LIMIT ', SelNum);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##建筑查询
 DROP PROCEDURE IF EXISTS  Building_GetByID;
##GO
CREATE PROCEDURE Building_GetByID(
ID int
)
BEGIN
	DECLARE Flag int;
	DECLARE SqlWhere varchar(100);
	IF EXISTS(SELECT * FROM Building WHERE ID=ID)
	THEN
		SELECT Flag=Flag FROM Building WHERE ID=ID;
		SET SqlWhere = 'ID='+ trim(ID);
		CALL  Building_GetBywhere_Num(Flag,1,SqlWhere);
	END IF;
END;
##GO

##建筑新增或修改
 DROP PROCEDURE IF EXISTS  Building_ADD_UPDATE;
##GO
CREATE PROCEDURE Building_ADD_UPDATE(
OUT RtnInfo varchar(500) ,
OUT NewID int,
Tag int,
iID int,
iBname varchar(50) ,
iFlag int,
iCode int,
iFID int,
iContact varchar(50) ,
iTel varchar(100) ,
iBuildingSerialNo varchar(50) 
)
BEGIN
	DECLARE FlagDesc varchar(20) ;
	DECLARE iAreaCode int;
	DECLARE iBuildCode int;
	DECLARE iUnitCode int;
	DECLARE iFloorCode int;
	DECLARE iRoomCode int;
	DECLARE iCount int;

	SET NewID = 0;
	SET RtnInfo = '';
	IF(Tag = 0 OR Tag = 1)##正确的操作类型
	THEN		
		IF(iFlag >= 0 AND iFlag <= 3)
		THEN
			SELECT CASE iFlag WHEN 0 THEN '小区' WHEN 1 THEN '楼栋' WHEN 2 THEN '单元' WHEN 3 THEN '房间' ELSE '未知类型' END INTO FlagDesc;
			IF(Tag = 0)##新增
			THEN
				SELECT COUNT(1) INTO iCount FROM Building WHERE FID=iFID AND Flag=iFlag AND Code=iCode;
                IF(iCount)
				THEN
                    SET RtnInfo = concat('系统中已经存在编码为', trim(iCode), '的' , trim(FlagDesc), '数据了!');
				ELSE
                    IF(iFlag=0 AND iBuildingSerialNo<>'')
					THEN
						SELECT COUNT(1) INTO iCount FROM Building WHERE Flag=iFlag AND BuildingSerialNo=iBuildingSerialNo;
                        IF(iCount)
						THEN
							SET RtnInfo = concat('系统中已经存在序列号为', trim(iBuildingSerialNo), '的', trim(FlagDesc), '数据了!');
						ELSE
							INSERT INTO Building (Bname, Flag, Code, FID, Contact, Tel, BuildingSerialNo)
                                VALUES(iBname, iFlag, iCode, iFID, iContact, iTel, iBuildingSerialNo);
							SET NewID = LAST_INSERT_ID();
						END IF;
					ELSE
						INSERT INTO Building (Bname, Flag, Code, FID, Contact, Tel, BuildingSerialNo)
                            VALUES(iBname, iFlag, iCode, iFID, iContact, iTel, iBuildingSerialNo);
						SET NewID = LAST_INSERT_ID();
					END IF;
					IF(NewID>0)
					THEN
						##自动把那些未归属的卡片归位
						IF(iFlag=0)##小区
						THEN
							##把巡更卡(1)\管理卡(2)；重新归位给小区
							UPDATE Card SET BuildingID=NewID WHERE (CardType=1 OR CardType=2) AND RAreaCode=iCode;
						ELSEIF(iFlag=3)##房间
						THEN
							##把用户卡(0)重新归位给房间
							SELECT AreaCode, BuildCode, UnitCode, Code INTO iAreaCode, iBuildCode, iUnitCode, iRoomCode 
                                FROM V_Room WHERE ID=NewID;
							UPDATE Card SET BuildingID=NewID 
                                WHERE CardType=0 AND RAreaCode=iAreaCode AND RBuildCode=iBuildCode AND RUnitCode=iUnitCode AND RRoomCode=iRoomCode;
                        END IF;
					END IF;
				END IF;
			ELSE ##修改
                SELECT COUNT(1) INTO iCount FROM Building WHERE FID=iFID AND Flag=iFlag AND Code=iCode AND ID<>iID;
				IF(iCount)
				THEN
                    SET RtnInfo = concat('系统中已经存在编码为', trim(iCode), '的' , trim(FlagDesc), '数据了!');
				ELSE
					IF(iFlag=0 AND iBuildingSerialNo<>'')
					THEN
                        SELECT COUNT(1) INTO iCount FROM Building WHERE Flag=iFlag AND BuildingSerialNo=iBuildingSerialNo AND ID<>iID;
						IF(iCount)
						THEN
                            SET RtnInfo = concat('系统中已经存在序列号为', trim(iBuildingSerialNo), '的' , trim(FlagDesc), '数据了!');
						ELSE
							UPDATE Building 
							SET Bname=iBname,Flag=iFlag,Code=iCode,FID=iFID,Contact=iContact,Tel=iTel,BuildingSerialNo=iBuildingSerialNo
							WHERE ID=iID;
							SET NewID = iID;
						END IF;
					ELSE
						UPDATE Building 
						SET Bname=iBname,Flag=iFlag,Code=iCode,FID=iFID,Contact=iContact,Tel=iTel,BuildingSerialNo=iBuildingSerialNo
						WHERE ID=iID;
						SET NewID = iID;
					END IF;
				END IF;
			END IF;
		ELSE
			SET RtnInfo = '非法类型';
		END IF;
	ELSE
		SET RtnInfo = '非法操作';
	END IF;
END;
##GO

##建筑删除
 DROP PROCEDURE IF EXISTS  Building_DeleteByID;
##GO

##通过递归查找所有子节点 删除
CREATE PROCEDURE Building_DeleteByID(
iID int
)
BEGIN
    DECLARE iChd varchar(1000);
    DECLARE iTempid varchar(1000);
    
    SET iChd = cast(iID as CHAR);
    WHILE iChd is not null DO
        ##查找当前节点的所有子节点
        SELECT group_concat(ID) INTO iTempid FROM Building WHERE FIND_IN_SET(FID, iChd);
        ##删除当前父节点
        DELETE FROM Building WHERE FIND_IN_SET(ID, iChd);
        ##将子节点作为父节点,用于下次循环
        SET iChd = iTempid;
    END WHILE;
END;
##GO

##卡片查询
 DROP PROCEDURE IF EXISTS  Card_GetBywhere_Num;
##GO
CREATE PROCEDURE Card_GetBywhere_Num(
SelNum int,
Where_String varchar(800)
)
BEGIN
	DECLARE strSQL varchar(4000);  ## 主语句
	SET @strSQL = concat('SELECT * FROM V_Card WHERE ', Where_String, ' LIMIT ', SelNum);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##卡片新增或修改
 DROP PROCEDURE IF EXISTS  Card_ADD_UPDATE;
##GO

CREATE PROCEDURE Card_ADD_UPDATE(
OUT RtnInfo varchar(500),
OUT NewID int,
iTag int,
iID int,
iRAreaCode int,
iRBuildCode int,
iRUnitCode int,
iRRoomCode int,
iCardNo bigint unsigned,
iCardType int,
iSerialNo varchar(50),
iExpiryDate bigint,
iIfFrezen bit,
iCreateDate bigint,
iContact varchar(200),
iTel varchar(200),
iMemo varchar(200)
)
BEGIN
	##卡只能属于房间或则小区
	DECLARE iBuildingID INT;
	SET iBuildingID = 0;
	SET NewID = 0;
	SET RtnInfo = '';
	IF(iTag = 0 OR iTag = 1)##正确的操作类型
	THEN
		IF(iCardType >= 0 AND iCardType <= 2)
		THEN
			##计算BuildingID
			IF(iCardType = 0)##户卡 属于房间
			THEN
				IF EXISTS(SELECT * FROM V_Room WHERE AreaCode=iRAreaCode AND BuildCode=iRBuildCode AND UnitCode=iRUnitCode AND Code=iRRoomCode)
				THEN
					SELECT ID into iBuildingID FROM V_Room WHERE AreaCode=iRAreaCode AND BuildCode=iRBuildCode AND UnitCode=iRUnitCode AND Code=iRRoomCode;
				END IF;
			ELSE##其他卡 属于小区
				IF EXISTS(SELECT * FROM V_Area WHERE Code=iRAreaCode)
				THEN
					SELECT ID into iBuildingID FROM V_Area WHERE Code=iRAreaCode;
				END IF;
			END IF;

			IF(iTag = 0) ##新增
			THEN
				IF NOT EXISTS(SELECT * FROM Card WHERE CardNo=iCardNo)
				THEN
					INSERT INTO Card (BuildingID, RAreaCode, RBuildCode, RUnitCode, RRoomCode, CardNo, CardType, SerialNo, ExpiryDate, IfFrezen, CreateDate, Contact, Tel, Memo)
					VALUES(iBuildingID, iRAreaCode, iRBuildCode, iRUnitCode, iRRoomCode, iCardNo, iCardType, iSerialNo, iExpiryDate, iIfFrezen, iCreateDate, iContact, iTel, iMemo) ;
					SET NewID = LAST_INSERT_ID();
				ELSE
					SET RtnInfo = '系统中已经存在卡号为' + iCardNo + '的卡片了'			;
				END IF;
			ELSE ##修改
                UPDATE Card 
                SET BuildingID=iBuildingID,RAreaCode=iRAreaCode,RBuildCode=iRBuildCode,RUnitCode=iRUnitCode,RRoomCode=iRRoomCode,CardNo=iCardNo,CardType=iCardType,SerialNo=iSerialNo,ExpiryDate=iExpiryDate,Contact=iContact,Tel=iTel,Memo=iMemo
                WHERE CardNo=iCardNo;
                ##SET NewID = iID;
			END IF;
		ELSE
			SET RtnInfo = '非法的卡片类型';
		END IF;
	ELSE
		SET RtnInfo = '非法操作';
	END IF;
END;
##GO

##卡片删除
 DROP PROCEDURE IF EXISTS  Card_DeleteByID;
##GO
CREATE PROCEDURE Card_DeleteByID(
iID int
)
BEGIN
	##删除刷卡日志CardLog
	##DELETE FROM CardLog WHERE CardID=ID;
	DELETE FROM Card WHERE ID=iID;
END;
##GO

##刷卡日志查询
 DROP PROCEDURE IF EXISTS  CardLog_GetBywhere_Num;
##GO
CREATE PROCEDURE CardLog_GetBywhere_Num(
SelNum int,
Where_String varchar(800)
)
BEGIN
	DECLARE strSQL varchar(4000);  ## 主语句
	SET @strSQL = concat('SELECT * FROM V_CardLog WHERE ', Where_String, ' LIMIT ', SelNum);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##刷卡日志新增或修改
 DROP PROCEDURE IF EXISTS  CardLog_ADD_UPDATE;
##GO

CREATE PROCEDURE CardLog_ADD_UPDATE(
OUT RtnInfo varchar(500),
OUT NewID int,
Tag int,
iID int,
iCardNo int unsigned,
iCardType int,
iDeviceType int,
iDeviceNo varchar(100),
iCreateDate int
)
BEGIN
	
	DECLARE iRAreaCode int;
	DECLARE iRBuildCode int;
	DECLARE iRUnitCode int;
	DECLARE iRRoomCode int	;
	DECLARE iSerialNo varchar(50);
	DECLARE iContact varchar(50);
	DECLARE iTel varchar(100);
	DECLARE iMemo varchar(200);
	
	SET NewID = 0;
	SET RtnInfo = '';
	IF(Tag = 0 OR Tag = 1)##正确的操作类型
	THEN
		IF(Tag = 0) ##新增
		THEN
			SET iRAreaCode = -1;
			SET iRBuildCode = -1;
			SET iRUnitCode = -1;
			SET iRRoomCode = -1;
			SET iSerialNo = '';
			SET iContact = '';
			SET iTel = '';
			SET iMemo = '';
			IF EXISTS(SELECT * FROM V_Card WHERE CardNO=iCardNo)
			THEN
				SELECT RAreaCode, RBuildCode, RUnitCode, RRoomCode, SerialNo, Contact, Tel, Memo into iRAreaCode, iRBuildCode, iRUnitCode, iRRoomCode, iSerialNo, iContact, iTel, iMemo FROM V_Card WHERE CardNO = iCardNo;
			END IF;
			INSERT INTO CardLog (CardNo, RAreaCode,RBuildCode, RUnitCode, RRoomCode, CardType, DeviceType, DeviceNo, SerialNo, CreateDate, Contact, Tel, Memo)
			VALUES(iCardNo, iRAreaCode, iRBuildCode, iRUnitCode, iRRoomCode, iCardType, iDeviceType, iDeviceNo, iSerialNo, iCreateDate, iContact, iTel, iMemo) ;
			SET NewID = LAST_INSERT_ID();
		END IF;
	ELSE
		SET RtnInfo = '非法操作';
	END IF;
END;
##GO

##刷卡日志删除
 DROP PROCEDURE IF EXISTS  CardLog_DeleteByWhere;
##GO
CREATE PROCEDURE CardLog_DeleteByWhere(
Where_String varchar(800)
)
BEGIN
	DECLARE strSQL varchar(4000);  ## 主语句
	SET @strSQL = concat('DELETE FROM CardLog ', Where_String);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##通讯日志查询
 DROP PROCEDURE IF EXISTS  CommLog_GetBywhere_Num;
##GO
CREATE PROCEDURE CommLog_GetBywhere_Num(
SelNum int,
Where_String varchar(800)
)
BEGIN
	DECLARE strSQL varchar(4000);  ## 主语句
	SET @strSQL = concat('SELECT * FROM V_CommLog WHERE ', Where_String, ' LIMIT ', SelNum);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##通讯日志新增
 DROP PROCEDURE IF EXISTS  CommLog_ADD_UPDATE;
##GO

CREATE PROCEDURE CommLog_ADD_UPDATE(
OUT ErrorInfo varchar(500),
OUT NewID int,
Tag int,
ID int,
Flag int,
Content text,
CreateDate int
)
BEGIN
	SET NewID = 0;
	SET ErrorInfo = '';
	IF(Tag = 0 OR Tag = 1)##正确的操作类型
	THEN
		IF(Tag = 0) ##新增
		THEN
			INSERT INTO CommLog (Flag, Content, CreateDate)
			VALUES(Flag, Content, CreateDate) ;
			SET NewID = LAST_INSERT_ID();
		END IF;
	ELSE
		SET ErrorInfo = '非法操作';
	END IF;
END;
##GO

##通讯日志删除
 DROP PROCEDURE IF EXISTS  CommLog_DeleteByWhere;
##GO
CREATE PROCEDURE CommLog_DeleteByWhere(
Where_String varchar(800)
)
BEGIN
	DECLARE strSQL varchar(4000);  ## 主语句
	SET @strSQL = concat('DELETE FROM CommLog ', Where_String);
    prepare stmt from @strSQL;  
    EXECUTE stmt;  
    deallocate prepare stmt;
END;
##GO

##删除数据
 DROP PROCEDURE IF EXISTS  DeleteData;
##GO
CREATE PROCEDURE DeleteData(
Flag int
)
BEGIN	
	DELETE FROM CardLog;
	DELETE FROM Card;
	DELETE FROM Building;
END;
##GO
#################################三。创建存储过程End#################################


#################################五。初始化数据Begin#################################
##用户表 默认123456
INSERT INTO UserInfo(UserName,UserPwd,Flag,IfFrezen) VALUES('admin','e10adc3949ba59abbe56e057f20f883e',0,0);
##GO
INSERT INTO UserInfo(UserName,UserPwd,Flag,IfFrezen) VALUES('test','e10adc3949ba59abbe56e057f20f883e',1,0);
##GO

##建筑表
INSERT INTO Building(Bname,Flag,Code,FID,BuildingSerialNo) VALUES('默认小区',0,1,0,'11223344556677889911111111111111');
##GO
#################################五。初始化数据End#################################