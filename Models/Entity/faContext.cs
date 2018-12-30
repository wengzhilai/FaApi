// using System;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata;

// namespace Models.Entity
// {
//     public partial class faContext : DbContext
//     {
//         public faContext()
//         {
//         }

//         public faContext(DbContextOptions<faContext> options)
//             : base(options)
//         {
//         }

//         public virtual DbSet<FaAppVersion> FaAppVersion { get; set; }
//         public virtual DbSet<FaBulletin> FaBulletin { get; set; }
//         public virtual DbSet<FaBulletinFile> FaBulletinFile { get; set; }
//         public virtual DbSet<FaBulletinLog> FaBulletinLog { get; set; }
//         public virtual DbSet<FaBulletinReview> FaBulletinReview { get; set; }
//         public virtual DbSet<FaBulletinRole> FaBulletinRole { get; set; }
//         public virtual DbSet<FaBulletinType> FaBulletinType { get; set; }
//         public virtual DbSet<FaConfig> FaConfig { get; set; }
//         public virtual DbSet<FaDbServer> FaDbServer { get; set; }
//         public virtual DbSet<FaDbServerType> FaDbServerType { get; set; }
//         public virtual DbSet<FaDistrict> FaDistrict { get; set; }
//         public virtual DbSet<FaDynasty> FaDynasty { get; set; }
//         public virtual DbSet<FaElder> FaElder { get; set; }
//         public virtual DbSet<FaEventFiles> FaEventFiles { get; set; }
//         public virtual DbSet<FaExportLog> FaExportLog { get; set; }
//         public virtual DbSet<FaFamily> FaFamily { get; set; }
//         public virtual DbSet<FaFiles> FaFiles { get; set; }
//         public virtual DbSet<FaFlow> FaFlow { get; set; }
//         public virtual DbSet<FaFlowFlownode> FaFlowFlownode { get; set; }
//         public virtual DbSet<FaFlowFlownodeFlow> FaFlowFlownodeFlow { get; set; }
//         public virtual DbSet<FaFlowFlownodeRole> FaFlowFlownodeRole { get; set; }
//         public virtual DbSet<FaFunction> FaFunction { get; set; }
//         public virtual DbSet<FaLog> FaLog { get; set; }
//         public virtual DbSet<FaLogin> FaLogin { get; set; }
//         public virtual DbSet<FaLoginHistory> FaLoginHistory { get; set; }
//         public virtual DbSet<FaMessage> FaMessage { get; set; }
//         public virtual DbSet<FaMessageType> FaMessageType { get; set; }
//         public virtual DbSet<FaModule> FaModule { get; set; }
//         public virtual DbSet<FaOauth> FaOauth { get; set; }
//         public virtual DbSet<FaOauthLogin> FaOauthLogin { get; set; }
//         public virtual DbSet<FaQuery> FaQuery { get; set; }
//         public virtual DbSet<FaRole> FaRole { get; set; }
//         public virtual DbSet<FaRoleConfig> FaRoleConfig { get; set; }
//         public virtual DbSet<FaRoleFunction> FaRoleFunction { get; set; }
//         public virtual DbSet<FaRoleModule> FaRoleModule { get; set; }
//         public virtual DbSet<FaRoleQueryAuthority> FaRoleQueryAuthority { get; set; }
//         public virtual DbSet<FaScript> FaScript { get; set; }
//         public virtual DbSet<FaScriptGroupList> FaScriptGroupList { get; set; }
//         public virtual DbSet<FaScriptTask> FaScriptTask { get; set; }
//         public virtual DbSet<FaScriptTaskLog> FaScriptTaskLog { get; set; }
//         public virtual DbSet<FaSmsSend> FaSmsSend { get; set; }
//         public virtual DbSet<FaTask> FaTask { get; set; }
//         public virtual DbSet<FaTaskFlow> FaTaskFlow { get; set; }
//         public virtual DbSet<FaTaskFlowHandle> FaTaskFlowHandle { get; set; }
//         public virtual DbSet<FaTaskFlowHandleFiles> FaTaskFlowHandleFiles { get; set; }
//         public virtual DbSet<FaTaskFlowHandleUser> FaTaskFlowHandleUser { get; set; }
//         public virtual DbSet<FaUpdataLog> FaUpdataLog { get; set; }
//         public virtual DbSet<FaUser> FaUser { get; set; }
//         public virtual DbSet<FaUserDistrict> FaUserDistrict { get; set; }
//         public virtual DbSet<FaUserEvent> FaUserEvent { get; set; }
//         public virtual DbSet<FaUserFile> FaUserFile { get; set; }
//         public virtual DbSet<FaUserFriend> FaUserFriend { get; set; }
//         public virtual DbSet<FaUserInfo> FaUserInfo { get; set; }
//         public virtual DbSet<FaUserRole> FaUserRole { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseMySql("server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;");
//             }
//         }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<FaAppVersion>(entity =>
//             {
//                 entity.ToTable("fa_app_version");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IsNew)
//                     .HasColumnName("IS_NEW")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(1000)");

//                 entity.Property(e => e.Type)
//                     .IsRequired()
//                     .HasColumnName("TYPE")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.UpdateTime)
//                     .HasColumnName("UPDATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.UpdateUrl)
//                     .HasColumnName("UPDATE_URL")
//                     .HasColumnType("varchar(200)");
//             });

//             modelBuilder.Entity<FaBulletin>(entity =>
//             {
//                 entity.ToTable("fa_bulletin");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AutoPen)
//                     .HasColumnName("AUTO_PEN")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.Content)
//                     .HasColumnName("CONTENT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.CreateTime)
//                     .HasColumnName("CREATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.IsImport)
//                     .HasColumnName("IS_IMPORT")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IsShow)
//                     .HasColumnName("IS_SHOW")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IsUrgent)
//                     .HasColumnName("IS_URGENT")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IssueDate)
//                     .HasColumnName("ISSUE_DATE")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Pic)
//                     .HasColumnName("PIC")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.Publisher)
//                     .IsRequired()
//                     .HasColumnName("PUBLISHER")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.Region)
//                     .IsRequired()
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Title)
//                     .IsRequired()
//                     .HasColumnName("TITLE")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.TypeCode)
//                     .HasColumnName("TYPE_CODE")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.UpdateTime)
//                     .HasColumnName("UPDATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<FaBulletinFile>(entity =>
//             {
//                 entity.HasKey(e => new { e.BulletinId, e.FileId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_bulletin_file");

//                 entity.HasIndex(e => e.FileId)
//                     .HasName("FK_FA_BULLETIN_FILE_REF_FILE");

//                 entity.Property(e => e.BulletinId)
//                     .HasColumnName("BULLETIN_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FileId)
//                     .HasColumnName("FILE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Bulletin)
//                     .WithMany(p => p.FaBulletinFile)
//                     .HasForeignKey(d => d.BulletinId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_file_ibfk_1");

//                 entity.HasOne(d => d.File)
//                     .WithMany(p => p.FaBulletinFile)
//                     .HasForeignKey(d => d.FileId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_file_ibfk_2");
//             });

//             modelBuilder.Entity<FaBulletinLog>(entity =>
//             {
//                 entity.ToTable("fa_bulletin_log");

//                 entity.HasIndex(e => e.BulletinId)
//                     .HasName("FK_BULLETIN_LOG_REF_BULLETIN");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.BulletinId)
//                     .HasColumnName("BULLETIN_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LookTime)
//                     .HasColumnName("LOOK_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Bulletin)
//                     .WithMany(p => p.FaBulletinLog)
//                     .HasForeignKey(d => d.BulletinId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_log_ibfk_1");
//             });

//             modelBuilder.Entity<FaBulletinReview>(entity =>
//             {
//                 entity.ToTable("fa_bulletin_review");

//                 entity.HasIndex(e => e.BulletinId)
//                     .HasName("FK_FA_BULLETIN_REVIEW_REF_BULL");

//                 entity.HasIndex(e => e.ParentId)
//                     .HasName("FK_FA_BULLETIN_RE_REF_REVIEW");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AddTime)
//                     .HasColumnName("ADD_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.BulletinId)
//                     .HasColumnName("BULLETIN_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Content)
//                     .HasColumnName("CONTENT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.ParentId)
//                     .HasColumnName("PARENT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Status)
//                     .IsRequired()
//                     .HasColumnName("STATUS")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.StatusTime)
//                     .HasColumnName("STATUS_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Bulletin)
//                     .WithMany(p => p.FaBulletinReview)
//                     .HasForeignKey(d => d.BulletinId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_review_ibfk_1");

//                 entity.HasOne(d => d.Parent)
//                     .WithMany(p => p.InverseParent)
//                     .HasForeignKey(d => d.ParentId)
//                     .HasConstraintName("fa_bulletin_review_ibfk_2");
//             });

//             modelBuilder.Entity<FaBulletinRole>(entity =>
//             {
//                 entity.HasKey(e => new { e.BulletinId, e.RoleId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_bulletin_role");

//                 entity.HasIndex(e => e.RoleId)
//                     .HasName("FK_FA_BULLETIN_ROLE_REF_ROLE");

//                 entity.Property(e => e.BulletinId)
//                     .HasColumnName("BULLETIN_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Bulletin)
//                     .WithMany(p => p.FaBulletinRole)
//                     .HasForeignKey(d => d.BulletinId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_role_ibfk_1");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaBulletinRole)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_role_ibfk_2");
//             });

//             modelBuilder.Entity<FaBulletinType>(entity =>
//             {
//                 entity.ToTable("fa_bulletin_type");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(80)");
//             });

//             modelBuilder.Entity<FaConfig>(entity =>
//             {
//                 entity.ToTable("fa_config");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AddTiem)
//                     .HasColumnName("ADD_TIEM")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.AddUserId)
//                     .HasColumnName("ADD_USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Code)
//                     .IsRequired()
//                     .HasColumnName("CODE")
//                     .HasColumnType("varchar(32)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Region)
//                     .IsRequired()
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.Type)
//                     .HasColumnName("TYPE")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Value)
//                     .HasColumnName("VALUE")
//                     .HasColumnType("varchar(300)");
//             });

//             modelBuilder.Entity<FaDbServer>(entity =>
//             {
//                 entity.ToTable("fa_db_server");

//                 entity.HasIndex(e => e.DbTypeId)
//                     .HasName("FK_FA_DB_SERVER_REF_TYPE");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.DbLink)
//                     .HasColumnName("DB_LINK")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.DbTypeId)
//                     .HasColumnName("DB_TYPE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Dbname)
//                     .HasColumnName("DBNAME")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Dbuid)
//                     .IsRequired()
//                     .HasColumnName("DBUID")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Ip)
//                     .IsRequired()
//                     .HasColumnName("IP")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Nickname)
//                     .HasColumnName("NICKNAME")
//                     .HasColumnType("varchar(32)");

//                 entity.Property(e => e.Password)
//                     .IsRequired()
//                     .HasColumnName("PASSWORD")
//                     .HasColumnType("varchar(32)");

//                 entity.Property(e => e.Port)
//                     .HasColumnName("PORT")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.ToPathD)
//                     .HasColumnName("TO_PATH_D")
//                     .HasColumnType("varchar(300)");

//                 entity.Property(e => e.ToPathM)
//                     .HasColumnName("TO_PATH_M")
//                     .HasColumnType("varchar(300)");

//                 entity.Property(e => e.Type)
//                     .IsRequired()
//                     .HasColumnName("TYPE")
//                     .HasColumnType("varchar(10)");

//                 entity.HasOne(d => d.DbType)
//                     .WithMany(p => p.FaDbServer)
//                     .HasForeignKey(d => d.DbTypeId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_db_server_ibfk_1");
//             });

//             modelBuilder.Entity<FaDbServerType>(entity =>
//             {
//                 entity.ToTable("fa_db_server_type");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");
//             });

//             modelBuilder.Entity<FaDistrict>(entity =>
//             {
//                 entity.ToTable("fa_district");

//                 entity.HasIndex(e => e.ParentId)
//                     .HasName("FK_FA_DISTRICT_REF_DISTRICT");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Code)
//                     .HasColumnName("CODE")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.IdPath)
//                     .HasColumnName("ID_PATH")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.InUse)
//                     .HasColumnName("IN_USE")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LevelId)
//                     .HasColumnName("LEVEL_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.ParentId)
//                     .HasColumnName("PARENT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Region)
//                     .IsRequired()
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.HasOne(d => d.Parent)
//                     .WithMany(p => p.InverseParent)
//                     .HasForeignKey(d => d.ParentId)
//                     .HasConstraintName("fa_district_ibfk_1");
//             });

//             modelBuilder.Entity<FaDynasty>(entity =>
//             {
//                 entity.ToTable("fa_dynasty");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(20)");
//             });

//             modelBuilder.Entity<FaElder>(entity =>
//             {
//                 entity.ToTable("fa_elder");

//                 entity.HasIndex(e => e.FamilyId)
//                     .HasName("FK_FA_ELDER_REF_FAMILY");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FamilyId)
//                     .HasColumnName("FAMILY_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(2)");

//                 entity.Property(e => e.Sort)
//                     .HasColumnName("SORT")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Family)
//                     .WithMany(p => p.FaElder)
//                     .HasForeignKey(d => d.FamilyId)
//                     .HasConstraintName("fa_elder_ibfk_1");
//             });

//             modelBuilder.Entity<FaEventFiles>(entity =>
//             {
//                 entity.HasKey(e => new { e.EventId, e.FilesId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_event_files");

//                 entity.HasIndex(e => e.FilesId)
//                     .HasName("FK_FA_EVENT_FILES_REF_FILES");

//                 entity.Property(e => e.EventId)
//                     .HasColumnName("EVENT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FilesId)
//                     .HasColumnName("FILES_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Event)
//                     .WithMany(p => p.FaEventFiles)
//                     .HasForeignKey(d => d.EventId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_event_files_ibfk_1");

//                 entity.HasOne(d => d.Files)
//                     .WithMany(p => p.FaEventFiles)
//                     .HasForeignKey(d => d.FilesId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_event_files_ibfk_2");
//             });

//             modelBuilder.Entity<FaExportLog>(entity =>
//             {
//                 entity.ToTable("fa_export_log");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.ExportTime)
//                     .HasColumnName("EXPORT_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.LoginName)
//                     .HasColumnName("LOGIN_NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.SqlContent)
//                     .HasColumnName("SQL_CONTENT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<FaFamily>(entity =>
//             {
//                 entity.ToTable("fa_family");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(20)");
//             });

//             modelBuilder.Entity<FaFiles>(entity =>
//             {
//                 entity.ToTable("fa_files");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FileType)
//                     .HasColumnName("FILE_TYPE")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Length)
//                     .HasColumnName("LENGTH")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.Path)
//                     .IsRequired()
//                     .HasColumnName("PATH")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(2000)");

//                 entity.Property(e => e.UploadTime)
//                     .HasColumnName("UPLOAD_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Url)
//                     .HasColumnName("URL")
//                     .HasColumnType("varchar(254)");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<FaFlow>(entity =>
//             {
//                 entity.ToTable("fa_flow");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FlowType)
//                     .IsRequired()
//                     .HasColumnName("FLOW_TYPE")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.Region)
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.XY)
//                     .HasColumnName("X_Y")
//                     .HasColumnType("varchar(500)");
//             });

//             modelBuilder.Entity<FaFlowFlownode>(entity =>
//             {
//                 entity.ToTable("fa_flow_flownode");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.HandleUrl)
//                     .HasColumnName("HANDLE_URL")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.ShowUrl)
//                     .HasColumnName("SHOW_URL")
//                     .HasColumnType("varchar(200)");
//             });

//             modelBuilder.Entity<FaFlowFlownodeFlow>(entity =>
//             {
//                 entity.ToTable("fa_flow_flownode_flow");

//                 entity.HasIndex(e => e.FlowId)
//                     .HasName("FK_FA_FLOWNODE_FLOW_REF_FLOW");

//                 entity.HasIndex(e => e.FromFlownodeId)
//                     .HasName("FK_FA_FLOWNODE_FLOW_REF_NODE");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Assigner)
//                     .HasColumnName("ASSIGNER")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.ExpireHour)
//                     .HasColumnName("EXPIRE_HOUR")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FlowId)
//                     .HasColumnName("FLOW_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FromFlownodeId)
//                     .HasColumnName("FROM_FLOWNODE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Handle)
//                     .HasColumnName("HANDLE")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Status)
//                     .HasColumnName("STATUS")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.ToFlownodeId)
//                     .HasColumnName("TO_FLOWNODE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Flow)
//                     .WithMany(p => p.FaFlowFlownodeFlow)
//                     .HasForeignKey(d => d.FlowId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_flow_ibfk_1");

//                 entity.HasOne(d => d.FromFlownode)
//                     .WithMany(p => p.FaFlowFlownodeFlow)
//                     .HasForeignKey(d => d.FromFlownodeId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_flow_ibfk_2");
//             });

//             modelBuilder.Entity<FaFlowFlownodeRole>(entity =>
//             {
//                 entity.HasKey(e => new { e.FlowId, e.RoleId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_flow_flownode_role");

//                 entity.HasIndex(e => e.RoleId)
//                     .HasName("FK_FA_FLOW_REF_ROLE");

//                 entity.Property(e => e.FlowId)
//                     .HasColumnName("FLOW_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Flow)
//                     .WithMany(p => p.FaFlowFlownodeRole)
//                     .HasForeignKey(d => d.FlowId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_role_ibfk_2");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaFlowFlownodeRole)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_role_ibfk_1");
//             });

//             modelBuilder.Entity<FaFunction>(entity =>
//             {
//                 entity.ToTable("fa_function");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.ClassName)
//                     .HasColumnName("CLASS_NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.DllName)
//                     .HasColumnName("DLL_NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.FullName)
//                     .HasColumnName("FULL_NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.MethodName)
//                     .HasColumnName("METHOD_NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.Namespace)
//                     .HasColumnName("NAMESPACE")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.XmlNote)
//                     .HasColumnName("XML_NOTE")
//                     .HasColumnType("varchar(254)");
//             });

//             modelBuilder.Entity<FaLog>(entity =>
//             {
//                 entity.ToTable("fa_log");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AddTime)
//                     .HasColumnName("ADD_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.ModuleName)
//                     .IsRequired()
//                     .HasColumnName("MODULE_NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<FaLogin>(entity =>
//             {
//                 entity.ToTable("fa_login");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.EmailAddr)
//                     .HasColumnName("EMAIL_ADDR")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.FailCount)
//                     .HasColumnName("FAIL_COUNT")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IsLocked)
//                     .HasColumnName("IS_LOCKED")
//                     .HasColumnType("int(1)");

//                 entity.Property(e => e.LockedReason)
//                     .HasColumnName("LOCKED_REASON")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.LoginName)
//                     .HasColumnName("LOGIN_NAME")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.PassUpdateDate)
//                     .HasColumnName("PASS_UPDATE_DATE")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Password)
//                     .HasColumnName("PASSWORD")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.PhoneNo)
//                     .HasColumnName("PHONE_NO")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.VerifyCode)
//                     .HasColumnName("VERIFY_CODE")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.VerifyTime)
//                     .HasColumnName("VERIFY_TIME")
//                     .HasColumnType("datetime");
//             });

//             modelBuilder.Entity<FaLoginHistory>(entity =>
//             {
//                 entity.ToTable("fa_login_history");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LoginHistoryType)
//                     .HasColumnName("LOGIN_HISTORY_TYPE")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LoginHost)
//                     .HasColumnName("LOGIN_HOST")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.LoginTime)
//                     .HasColumnName("LOGIN_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.LogoutTime)
//                     .HasColumnName("LOGOUT_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Message)
//                     .HasColumnName("MESSAGE")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<FaMessage>(entity =>
//             {
//                 entity.ToTable("fa_message");

//                 entity.HasIndex(e => e.MessageTypeId)
//                     .HasName("FK_FA_MESSAGE_REF_MESSAGE_TYPE");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AllRoleId)
//                     .HasColumnName("ALL_ROLE_ID")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.Content)
//                     .HasColumnName("CONTENT")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.CreateTime)
//                     .HasColumnName("CREATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.CreateUserid)
//                     .HasColumnName("CREATE_USERID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateUsername)
//                     .HasColumnName("CREATE_USERNAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.DistrictId)
//                     .HasColumnName("DISTRICT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.KeyId)
//                     .HasColumnName("KEY_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.MessageTypeId)
//                     .HasColumnName("MESSAGE_TYPE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.PushType)
//                     .HasColumnName("PUSH_TYPE")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Status)
//                     .HasColumnName("STATUS")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Title)
//                     .HasColumnName("TITLE")
//                     .HasColumnType("varchar(100)");

//                 entity.HasOne(d => d.MessageType)
//                     .WithMany(p => p.FaMessage)
//                     .HasForeignKey(d => d.MessageTypeId)
//                     .HasConstraintName("fa_message_ibfk_1");
//             });

//             modelBuilder.Entity<FaMessageType>(entity =>
//             {
//                 entity.ToTable("fa_message_type");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IsUse)
//                     .HasColumnName("IS_USE")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.TableName)
//                     .HasColumnName("TABLE_NAME")
//                     .HasColumnType("varchar(50)");
//             });

//             modelBuilder.Entity<FaModule>(entity =>
//             {
//                 entity.ToTable("fa_module");

//                 entity.HasIndex(e => e.ParentId)
//                     .HasName("FK_FA_MODULE_REF_MODULE");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Code)
//                     .HasColumnName("CODE")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Description)
//                     .HasColumnName("DESCRIPTION")
//                     .HasColumnType("varchar(2000)");

//                 entity.Property(e => e.DesktopRole)
//                     .HasColumnName("DESKTOP_ROLE")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.H).HasColumnType("int(11)");

//                 entity.Property(e => e.ImageUrl)
//                     .HasColumnName("IMAGE_URL")
//                     .HasColumnType("varchar(2000)");

//                 entity.Property(e => e.IsDebug)
//                     .HasColumnName("IS_DEBUG")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IsHide)
//                     .HasColumnName("IS_HIDE")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.Location)
//                     .HasColumnName("LOCATION")
//                     .HasColumnType("varchar(2000)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(60)");

//                 entity.Property(e => e.ParentId)
//                     .HasColumnName("PARENT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.ShowOrder)
//                     .HasColumnName("SHOW_ORDER")
//                     .HasColumnType("decimal(2,0)");

//                 entity.Property(e => e.W).HasColumnType("int(11)");

//                 entity.HasOne(d => d.Parent)
//                     .WithMany(p => p.InverseParent)
//                     .HasForeignKey(d => d.ParentId)
//                     .HasConstraintName("fa_module_ibfk_1");
//             });

//             modelBuilder.Entity<FaOauth>(entity =>
//             {
//                 entity.ToTable("fa_oauth");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LoginUrl)
//                     .HasColumnName("LOGIN_URL")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.RegUrl)
//                     .HasColumnName("REG_URL")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");
//             });

//             modelBuilder.Entity<FaOauthLogin>(entity =>
//             {
//                 entity.HasKey(e => new { e.OauthId, e.LoginId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_oauth_login");

//                 entity.HasIndex(e => e.LoginId)
//                     .HasName("FK_FA_OAUTH_REF_LOGIN");

//                 entity.Property(e => e.OauthId)
//                     .HasColumnName("OAUTH_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LoginId)
//                     .HasColumnName("LOGIN_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Login)
//                     .WithMany(p => p.FaOauthLogin)
//                     .HasForeignKey(d => d.LoginId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_oauth_login_ibfk_1");

//                 entity.HasOne(d => d.Oauth)
//                     .WithMany(p => p.FaOauthLogin)
//                     .HasForeignKey(d => d.OauthId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_oauth_login_ibfk_2");
//             });

//             modelBuilder.Entity<FaQuery>(entity =>
//             {
//                 entity.ToTable("fa_query");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AutoLoad)
//                     .HasColumnName("AUTO_LOAD")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.ChartsCfg)
//                     .HasColumnName("CHARTS_CFG")
//                     .HasColumnType("text");

//                 entity.Property(e => e.ChartsType)
//                     .HasColumnName("CHARTS_TYPE")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Code)
//                     .IsRequired()
//                     .HasColumnName("CODE")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.DbServerId)
//                     .HasColumnName("DB_SERVER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FiltrLevel)
//                     .HasColumnName("FILTR_LEVEL")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.FiltrStr)
//                     .HasColumnName("FILTR_STR")
//                     .HasColumnType("text");

//                 entity.Property(e => e.HeardBtn)
//                     .HasColumnName("HEARD_BTN")
//                     .HasColumnType("text");

//                 entity.Property(e => e.InParaJson)
//                     .HasColumnName("IN_PARA_JSON")
//                     .HasColumnType("text");

//                 entity.Property(e => e.IsDebug)
//                     .HasColumnName("IS_DEBUG")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.JsStr)
//                     .HasColumnName("JS_STR")
//                     .HasColumnType("text");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.NewData)
//                     .HasColumnName("NEW_DATA")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.PageSize)
//                     .HasColumnName("PAGE_SIZE")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.QueryCfgJson)
//                     .HasColumnName("QUERY_CFG_JSON")
//                     .HasColumnType("text");

//                 entity.Property(e => e.QueryConf)
//                     .HasColumnName("QUERY_CONF")
//                     .HasColumnType("text");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("text");

//                 entity.Property(e => e.ReportScript)
//                     .HasColumnName("REPORT_SCRIPT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.RowsBtn)
//                     .HasColumnName("ROWS_BTN")
//                     .HasColumnType("text");

//                 entity.Property(e => e.ShowCheckbox)
//                     .HasColumnName("SHOW_CHECKBOX")
//                     .HasColumnType("decimal(1,0)");
//             });

//             modelBuilder.Entity<FaRole>(entity =>
//             {
//                 entity.ToTable("fa_role");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(80)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.Type)
//                     .HasColumnName("TYPE")
//                     .HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<FaRoleConfig>(entity =>
//             {
//                 entity.ToTable("fa_role_config");

//                 entity.HasIndex(e => e.RoleId)
//                     .HasName("FK_FA_ROLE_CONFIG_REF_ROLE");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Type)
//                     .HasColumnName("TYPE")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Value)
//                     .HasColumnName("VALUE")
//                     .HasColumnType("varchar(300)");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaRoleConfig)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_config_ibfk_1");
//             });

//             modelBuilder.Entity<FaRoleFunction>(entity =>
//             {
//                 entity.HasKey(e => new { e.FunctionId, e.RoleId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_role_function");

//                 entity.HasIndex(e => e.RoleId)
//                     .HasName("FK_FA_ROLE_FUNCTION_REF_ROLE");

//                 entity.Property(e => e.FunctionId)
//                     .HasColumnName("FUNCTION_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Function)
//                     .WithMany(p => p.FaRoleFunction)
//                     .HasForeignKey(d => d.FunctionId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_function_ibfk_1");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaRoleFunction)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_function_ibfk_2");
//             });

//             modelBuilder.Entity<FaRoleModule>(entity =>
//             {
//                 entity.HasKey(e => new { e.RoleId, e.ModuleId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_role_module");

//                 entity.HasIndex(e => e.ModuleId)
//                     .HasName("FK_FA_ROLE_MODULE_REF_MODULE");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.ModuleId)
//                     .HasColumnName("MODULE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Module)
//                     .WithMany(p => p.FaRoleModule)
//                     .HasForeignKey(d => d.ModuleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_module_ibfk_1");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaRoleModule)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_module_ibfk_2");
//             });

//             modelBuilder.Entity<FaRoleQueryAuthority>(entity =>
//             {
//                 entity.HasKey(e => new { e.RoleId, e.QueryId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_role_query_authority");

//                 entity.HasIndex(e => e.QueryId)
//                     .HasName("FK_FA_ROLE_QUERY_REF_QUERY");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.QueryId)
//                     .HasColumnName("QUERY_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.NoAuthority)
//                     .HasColumnName("NO_AUTHORITY")
//                     .HasColumnType("varchar(200)");

//                 entity.HasOne(d => d.Query)
//                     .WithMany(p => p.FaRoleQueryAuthority)
//                     .HasForeignKey(d => d.QueryId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_query_authority_ibfk_1");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaRoleQueryAuthority)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_query_authority_ibfk_2");
//             });

//             modelBuilder.Entity<FaScript>(entity =>
//             {
//                 entity.ToTable("fa_script");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.BodyHash)
//                     .IsRequired()
//                     .HasColumnName("BODY_HASH")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.BodyText)
//                     .IsRequired()
//                     .HasColumnName("BODY_TEXT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.Code)
//                     .IsRequired()
//                     .HasColumnName("CODE")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.DisableReason)
//                     .HasColumnName("DISABLE_REASON")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.IsGroup)
//                     .HasColumnName("IS_GROUP")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.Region)
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.RunArgs)
//                     .HasColumnName("RUN_ARGS")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.RunData)
//                     .IsRequired()
//                     .HasColumnName("RUN_DATA")
//                     .HasColumnType("varchar(20)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RunWhen)
//                     .HasColumnName("RUN_WHEN")
//                     .HasColumnType("varchar(30)");

//                 entity.Property(e => e.ServiceFlag)
//                     .HasColumnName("SERVICE_FLAG")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.Status)
//                     .HasColumnName("STATUS")
//                     .HasColumnType("varchar(10)");
//             });

//             modelBuilder.Entity<FaScriptGroupList>(entity =>
//             {
//                 entity.HasKey(e => new { e.ScriptId, e.GroupId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_script_group_list");

//                 entity.HasIndex(e => e.GroupId)
//                     .HasName("FK_FA_GROUP_LIST_REF_SCRIPT");

//                 entity.Property(e => e.ScriptId)
//                     .HasColumnName("SCRIPT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.GroupId)
//                     .HasColumnName("GROUP_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.OrderIndex)
//                     .HasColumnName("ORDER_INDEX")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Group)
//                     .WithMany(p => p.FaScriptGroupList)
//                     .HasForeignKey(d => d.GroupId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_script_group_list_ibfk_1");
//             });

//             modelBuilder.Entity<FaScriptTask>(entity =>
//             {
//                 entity.ToTable("fa_script_task");

//                 entity.HasIndex(e => e.ScriptId)
//                     .HasName("FK_FA_SCRIPT_TASK_REF_SCRIPT");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.BodyHash)
//                     .IsRequired()
//                     .HasColumnName("BODY_HASH")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.BodyText)
//                     .IsRequired()
//                     .HasColumnName("BODY_TEXT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.DisableDate)
//                     .HasColumnName("DISABLE_DATE")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.DisableReason)
//                     .HasColumnName("DISABLE_REASON")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.DslType)
//                     .HasColumnName("DSL_TYPE")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.EndTime)
//                     .HasColumnName("END_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.GroupId)
//                     .HasColumnName("GROUP_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LogType)
//                     .HasColumnName("LOG_TYPE")
//                     .HasColumnType("decimal(1,0)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.Region)
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.ReturnCode)
//                     .HasColumnName("RETURN_CODE")
//                     .HasColumnType("varchar(10)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RunArgs)
//                     .HasColumnName("RUN_ARGS")
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.RunData)
//                     .IsRequired()
//                     .HasColumnName("RUN_DATA")
//                     .HasColumnType("varchar(20)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RunState)
//                     .IsRequired()
//                     .HasColumnName("RUN_STATE")
//                     .HasColumnType("varchar(10)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RunWhen)
//                     .HasColumnName("RUN_WHEN")
//                     .HasColumnType("varchar(30)");

//                 entity.Property(e => e.ScriptId)
//                     .HasColumnName("SCRIPT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.ServiceFlag)
//                     .HasColumnName("SERVICE_FLAG")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.StartTime)
//                     .HasColumnName("START_TIME")
//                     .HasColumnType("datetime");

//                 entity.HasOne(d => d.Script)
//                     .WithMany(p => p.FaScriptTask)
//                     .HasForeignKey(d => d.ScriptId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_script_task_ibfk_1");
//             });

//             modelBuilder.Entity<FaScriptTaskLog>(entity =>
//             {
//                 entity.ToTable("fa_script_task_log");

//                 entity.HasIndex(e => e.ScriptTaskId)
//                     .HasName("FK_FA_SCRIPT_TASK_LOG_REF_TASK");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LogTime)
//                     .HasColumnName("LOG_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.LogType)
//                     .HasColumnName("LOG_TYPE")
//                     .HasColumnType("decimal(1,0)")
//                     .HasDefaultValueSql("'1'");

//                 entity.Property(e => e.Message)
//                     .HasColumnName("MESSAGE")
//                     .HasColumnType("text");

//                 entity.Property(e => e.ScriptTaskId)
//                     .HasColumnName("SCRIPT_TASK_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.SqlText)
//                     .HasColumnName("SQL_TEXT")
//                     .HasColumnType("text");

//                 entity.HasOne(d => d.ScriptTask)
//                     .WithMany(p => p.FaScriptTaskLog)
//                     .HasForeignKey(d => d.ScriptTaskId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_script_task_log_ibfk_1");
//             });

//             modelBuilder.Entity<FaSmsSend>(entity =>
//             {
//                 entity.HasKey(e => e.Guid)
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_sms_send");

//                 entity.Property(e => e.Guid)
//                     .HasColumnName("GUID")
//                     .HasColumnType("char(32)");

//                 entity.Property(e => e.AddTime)
//                     .HasColumnName("ADD_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Content)
//                     .IsRequired()
//                     .HasColumnName("CONTENT")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.MessageId)
//                     .HasColumnName("MESSAGE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.PhoneNo)
//                     .IsRequired()
//                     .HasColumnName("PHONE_NO")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.SendTime)
//                     .HasColumnName("SEND_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Stauts)
//                     .HasColumnName("STAUTS")
//                     .HasColumnType("varchar(15)");

//                 entity.Property(e => e.TryNum)
//                     .HasColumnName("TRY_NUM")
//                     .HasColumnType("int(11)")
//                     .HasDefaultValueSql("'0'");
//             });

//             modelBuilder.Entity<FaTask>(entity =>
//             {
//                 entity.ToTable("fa_task");

//                 entity.HasIndex(e => e.FlowId)
//                     .HasName("FK_FA_FLOW_TASK_REF_FLOW");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateTime)
//                     .HasColumnName("CREATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.CreateUser)
//                     .HasColumnName("CREATE_USER")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateUserName)
//                     .HasColumnName("CREATE_USER_NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.DealTime)
//                     .HasColumnName("DEAL_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.EndTime)
//                     .HasColumnName("END_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.FlowId)
//                     .HasColumnName("FLOW_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.KeyId)
//                     .HasColumnName("KEY_ID")
//                     .HasColumnType("varchar(32)");

//                 entity.Property(e => e.Region)
//                     .HasColumnName("REGION")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("text");

//                 entity.Property(e => e.RoleIdStr)
//                     .HasColumnName("ROLE_ID_STR")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.StartTime)
//                     .HasColumnName("START_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Status)
//                     .HasColumnName("STATUS")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.StatusTime)
//                     .HasColumnName("STATUS_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.TaskName)
//                     .HasColumnName("TASK_NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.HasOne(d => d.Flow)
//                     .WithMany(p => p.FaTask)
//                     .HasForeignKey(d => d.FlowId)
//                     .HasConstraintName("fa_task_ibfk_1");
//             });

//             modelBuilder.Entity<FaTaskFlow>(entity =>
//             {
//                 entity.ToTable("fa_task_flow");

//                 entity.HasIndex(e => e.ParentId)
//                     .HasName("FK_FA_TASK_FLOW_REF_TASK_FLOW");

//                 entity.HasIndex(e => e.TaskId)
//                     .HasName("FK_FA_TASK_FLOW_REF_TASK");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.AcceptTime)
//                     .HasColumnName("ACCEPT_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.DealStatus)
//                     .HasColumnName("DEAL_STATUS")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.DealTime)
//                     .HasColumnName("DEAL_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.EqualId)
//                     .HasColumnName("EQUAL_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.ExpireTime)
//                     .HasColumnName("EXPIRE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.FlownodeId)
//                     .HasColumnName("FLOWNODE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.HandleUrl)
//                     .HasColumnName("HANDLE_URL")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.HandleUserId)
//                     .HasColumnName("HANDLE_USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IsHandle)
//                     .HasColumnName("IS_HANDLE")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LevelId)
//                     .HasColumnName("LEVEL_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.ParentId)
//                     .HasColumnName("PARENT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.RoleIdStr)
//                     .HasColumnName("ROLE_ID_STR")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.ShowUrl)
//                     .HasColumnName("SHOW_URL")
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.StartTime)
//                     .HasColumnName("START_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.TaskId)
//                     .HasColumnName("TASK_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Parent)
//                     .WithMany(p => p.InverseParent)
//                     .HasForeignKey(d => d.ParentId)
//                     .HasConstraintName("fa_task_flow_ibfk_2");

//                 entity.HasOne(d => d.Task)
//                     .WithMany(p => p.FaTaskFlow)
//                     .HasForeignKey(d => d.TaskId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_ibfk_1");
//             });

//             modelBuilder.Entity<FaTaskFlowHandle>(entity =>
//             {
//                 entity.ToTable("fa_task_flow_handle");

//                 entity.HasIndex(e => e.TaskFlowId)
//                     .HasName("FK_TASK_FLOW_HANDLE_REF_FLOW");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Content)
//                     .IsRequired()
//                     .HasColumnName("CONTENT")
//                     .HasColumnType("varchar(2000)");

//                 entity.Property(e => e.DealTime)
//                     .HasColumnName("DEAL_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.DealUserId)
//                     .HasColumnName("DEAL_USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.DealUserName)
//                     .IsRequired()
//                     .HasColumnName("DEAL_USER_NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.TaskFlowId)
//                     .HasColumnName("TASK_FLOW_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.TaskFlow)
//                     .WithMany(p => p.FaTaskFlowHandle)
//                     .HasForeignKey(d => d.TaskFlowId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_ibfk_1");
//             });

//             modelBuilder.Entity<FaTaskFlowHandleFiles>(entity =>
//             {
//                 entity.HasKey(e => new { e.FlowHandleId, e.FilesId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_task_flow_handle_files");

//                 entity.HasIndex(e => e.FilesId)
//                     .HasName("FK_FLOW_HANDLE_REF_FILES");

//                 entity.Property(e => e.FlowHandleId)
//                     .HasColumnName("FLOW_HANDLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FilesId)
//                     .HasColumnName("FILES_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Files)
//                     .WithMany(p => p.FaTaskFlowHandleFiles)
//                     .HasForeignKey(d => d.FilesId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_files_ibfk_1");

//                 entity.HasOne(d => d.FlowHandle)
//                     .WithMany(p => p.FaTaskFlowHandleFiles)
//                     .HasForeignKey(d => d.FlowHandleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_files_ibfk_2");
//             });

//             modelBuilder.Entity<FaTaskFlowHandleUser>(entity =>
//             {
//                 entity.HasKey(e => new { e.TaskFlowId, e.HandleUserId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_task_flow_handle_user");

//                 entity.Property(e => e.TaskFlowId)
//                     .HasColumnName("TASK_FLOW_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.HandleUserId)
//                     .HasColumnName("HANDLE_USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.TaskFlow)
//                     .WithMany(p => p.FaTaskFlowHandleUser)
//                     .HasForeignKey(d => d.TaskFlowId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_user_ibfk_1");
//             });

//             modelBuilder.Entity<FaUpdataLog>(entity =>
//             {
//                 entity.ToTable("fa_updata_log");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateTime)
//                     .HasColumnName("CREATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.CreateUserId)
//                     .HasColumnName("CREATE_USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateUserName)
//                     .HasColumnName("CREATE_USER_NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.NewContent)
//                     .HasColumnName("NEW_CONTENT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.OldContent)
//                     .HasColumnName("OLD_CONTENT")
//                     .HasColumnType("text");

//                 entity.Property(e => e.TableName)
//                     .HasColumnName("TABLE_NAME")
//                     .HasColumnType("varchar(50)");
//             });

//             modelBuilder.Entity<FaUser>(entity =>
//             {
//                 entity.ToTable("fa_user");

//                 entity.HasIndex(e => e.DistrictId)
//                     .HasName("FK_FA_USER_REF_DISTRICT");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateTime)
//                     .HasColumnName("CREATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.DistrictId)
//                     .HasColumnName("DISTRICT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IconFilesId)
//                     .HasColumnName("ICON_FILES_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IsLocked)
//                     .HasColumnName("IS_LOCKED")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LastActiveTime)
//                     .HasColumnName("LAST_ACTIVE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.LastLoginTime)
//                     .HasColumnName("LAST_LOGIN_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.LastLogoutTime)
//                     .HasColumnName("LAST_LOGOUT_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.LoginCount)
//                     .HasColumnName("LOGIN_COUNT")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LoginName)
//                     .HasColumnName("LOGIN_NAME")
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(80)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(2000)");

//                 entity.HasOne(d => d.District)
//                     .WithMany(p => p.FaUser)
//                     .HasForeignKey(d => d.DistrictId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_ibfk_1");
//             });

//             modelBuilder.Entity<FaUserDistrict>(entity =>
//             {
//                 entity.HasKey(e => new { e.UserId, e.DistrictId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_user_district");

//                 entity.HasIndex(e => e.DistrictId)
//                     .HasName("FK_FA_USER_DISTRICT_REF_DIST");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.DistrictId)
//                     .HasColumnName("DISTRICT_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.District)
//                     .WithMany(p => p.FaUserDistrict)
//                     .HasForeignKey(d => d.DistrictId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_district_ibfk_1");

//                 entity.HasOne(d => d.User)
//                     .WithMany(p => p.FaUserDistrict)
//                     .HasForeignKey(d => d.UserId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_district_ibfk_2");
//             });

//             modelBuilder.Entity<FaUserEvent>(entity =>
//             {
//                 entity.ToTable("fa_user_event");

//                 entity.HasIndex(e => e.UserId)
//                     .HasName("FK_FA_USER_EVENT_REF_USER");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Address)
//                     .HasColumnName("ADDRESS")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.Content)
//                     .HasColumnName("CONTENT")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.HappenTime)
//                     .HasColumnName("HAPPEN_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.Name)
//                     .HasColumnName("NAME")
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.User)
//                     .WithMany(p => p.FaUserEvent)
//                     .HasForeignKey(d => d.UserId)
//                     .HasConstraintName("fa_user_event_ibfk_1");
//             });

//             modelBuilder.Entity<FaUserFile>(entity =>
//             {
//                 entity.HasKey(e => new { e.UserId, e.FileId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_user_file");

//                 entity.HasIndex(e => e.FileId)
//                     .HasName("FK_FA_USER_FILE_REF_FILE");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FileId)
//                     .HasColumnName("FILE_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.File)
//                     .WithMany(p => p.FaUserFile)
//                     .HasForeignKey(d => d.FileId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_file_ibfk_1");

//                 entity.HasOne(d => d.User)
//                     .WithMany(p => p.FaUserFile)
//                     .HasForeignKey(d => d.UserId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_file_ibfk_2");
//             });

//             modelBuilder.Entity<FaUserFriend>(entity =>
//             {
//                 entity.HasKey(e => new { e.UserId, e.FriendId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_user_friend");

//                 entity.HasIndex(e => e.FriendId)
//                     .HasName("FK_FA_FRIEND_REF_USER");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FriendId)
//                     .HasColumnName("FRIEND_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Friend)
//                     .WithMany(p => p.FaUserFriend)
//                     .HasForeignKey(d => d.FriendId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_friend_ibfk_1");

//                 entity.HasOne(d => d.User)
//                     .WithMany(p => p.FaUserFriend)
//                     .HasForeignKey(d => d.UserId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_friend_ibfk_2");
//             });

//             modelBuilder.Entity<FaUserInfo>(entity =>
//             {
//                 entity.ToTable("fa_user_info");

//                 entity.Property(e => e.Id)
//                     .HasColumnName("ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Alias)
//                     .HasColumnName("ALIAS")
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.Authority)
//                     .HasColumnName("AUTHORITY")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.BirthdayPlace)
//                     .HasColumnName("BIRTHDAY_PLACE")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.BirthdayTime)
//                     .HasColumnName("BIRTHDAY_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.ConsortId)
//                     .HasColumnName("CONSORT_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CoupleId)
//                     .HasColumnName("COUPLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.CreateTime)
//                     .HasColumnName("CREATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.CreateUserId)
//                     .HasColumnName("CREATE_USER_ID")
//                     .HasColumnType("int(11)")
//                     .HasDefaultValueSql("'1'");

//                 entity.Property(e => e.CreateUserName)
//                     .IsRequired()
//                     .HasColumnName("CREATE_USER_NAME")
//                     .HasColumnType("varchar(50)")
//                     .HasDefaultValueSql("'admin'");

//                 entity.Property(e => e.DiedPlace)
//                     .HasColumnName("DIED_PLACE")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.DiedTime)
//                     .HasColumnName("DIED_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.ElderId)
//                     .HasColumnName("ELDER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FamilyId)
//                     .HasColumnName("FAMILY_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.FatherId)
//                     .HasColumnName("FATHER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.IsLive)
//                     .HasColumnName("IS_LIVE")
//                     .HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LevelId)
//                     .HasColumnName("LEVEL_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.LevelName)
//                     .HasColumnName("LEVEL_NAME")
//                     .HasColumnType("varchar(2)");

//                 entity.Property(e => e.MotherId)
//                     .HasColumnName("MOTHER_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.Remark)
//                     .HasColumnName("REMARK")
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.Sex)
//                     .HasColumnName("SEX")
//                     .HasColumnType("varchar(2)");

//                 entity.Property(e => e.Status)
//                     .IsRequired()
//                     .HasColumnName("STATUS")
//                     .HasColumnType("varchar(10)")
//                     .HasDefaultValueSql("'正常'");

//                 entity.Property(e => e.UpdateTime)
//                     .HasColumnName("UPDATE_TIME")
//                     .HasColumnType("datetime");

//                 entity.Property(e => e.UpdateUserId)
//                     .HasColumnName("UPDATE_USER_ID")
//                     .HasColumnType("int(11)")
//                     .HasDefaultValueSql("'1'");

//                 entity.Property(e => e.UpdateUserName)
//                     .IsRequired()
//                     .HasColumnName("UPDATE_USER_NAME")
//                     .HasColumnType("varchar(50)")
//                     .HasDefaultValueSql("'admin'");

//                 entity.Property(e => e.YearsType)
//                     .HasColumnName("YEARS_TYPE")
//                     .HasColumnType("varchar(10)");
//             });

//             modelBuilder.Entity<FaUserRole>(entity =>
//             {
//                 entity.HasKey(e => new { e.RoleId, e.UserId })
//                     .HasName("PRIMARY");

//                 entity.ToTable("fa_user_role");

//                 entity.HasIndex(e => e.UserId)
//                     .HasName("FK_FA_USER_ROLE_REF_USER");

//                 entity.Property(e => e.RoleId)
//                     .HasColumnName("ROLE_ID")
//                     .HasColumnType("int(11)");

//                 entity.Property(e => e.UserId)
//                     .HasColumnName("USER_ID")
//                     .HasColumnType("int(11)");

//                 entity.HasOne(d => d.Role)
//                     .WithMany(p => p.FaUserRole)
//                     .HasForeignKey(d => d.RoleId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("FK_FA_USER_ROLE_REF_ROLE");

//                 entity.HasOne(d => d.User)
//                     .WithMany(p => p.FaUserRole)
//                     .HasForeignKey(d => d.UserId)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("FK_FA_USER_ROLE_REF_USER");
//             });
//         }
//     }
// }
