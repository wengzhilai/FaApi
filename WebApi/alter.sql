

insert into fa_family_books(ID,FAMILY_ID,NAME,SORT,TYPE_ID,UserID,FileID) 
select (@rownum :=@rownum + 1) AS ID,1 FAMILY_ID, b.`NAME`,@rownum SORT, 2 TYPE_ID,a.ID UserID,0 FileID from fa_user_info a left join fa_user b on a.ID=b.ID,(SELECT @rownum := 90) run 
where b.`NAME`='翁先纭'

insert into fa_family_books(ID,FAMILY_ID,NAME,SORT,TYPE_ID,UserID,FileID) 
select (@rownum :=@rownum + 1) AS ID,1 FAMILY_ID, b.`NAME`,@rownum SORT, 2 TYPE_ID,a.ID UserID,0 FileID from fa_user_info a left join fa_user b on a.ID=b.ID,(SELECT @rownum := 91) run 
where a.ELDER_ID=21

insert into fa_family_books(ID,FAMILY_ID,NAME,SORT,TYPE_ID,UserID,FileID) 
select (@rownum :=@rownum + 1) AS ID,1 FAMILY_ID, b.`NAME`,@rownum SORT, 2 TYPE_ID,a.ID UserID,0 FileID from fa_user_info a left join fa_user b on a.ID=b.ID,(SELECT @rownum := 140) run 
where a.ID in (1288,1297,1367)

廿