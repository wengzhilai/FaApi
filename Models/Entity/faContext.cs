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

//         public virtual DbSet<fa_app_version> fa_app_version { get; set; }
//         public virtual DbSet<fa_bulletin> fa_bulletin { get; set; }
//         public virtual DbSet<fa_bulletin_file> fa_bulletin_file { get; set; }
//         public virtual DbSet<fa_bulletin_log> fa_bulletin_log { get; set; }
//         public virtual DbSet<fa_bulletin_review> fa_bulletin_review { get; set; }
//         public virtual DbSet<fa_bulletin_role> fa_bulletin_role { get; set; }
//         public virtual DbSet<fa_bulletin_type> fa_bulletin_type { get; set; }
//         public virtual DbSet<fa_config> fa_config { get; set; }
//         public virtual DbSet<fa_db_server> fa_db_server { get; set; }
//         public virtual DbSet<fa_db_server_type> fa_db_server_type { get; set; }
//         public virtual DbSet<fa_district> fa_district { get; set; }
//         public virtual DbSet<fa_dynasty> fa_dynasty { get; set; }
//         public virtual DbSet<fa_elder> fa_elder { get; set; }
//         public virtual DbSet<fa_event_files> fa_event_files { get; set; }
//         public virtual DbSet<fa_export_log> fa_export_log { get; set; }
//         public virtual DbSet<fa_family> fa_family { get; set; }
//         public virtual DbSet<fa_files> fa_files { get; set; }
//         public virtual DbSet<fa_flow> fa_flow { get; set; }
//         public virtual DbSet<fa_flow_flownode> fa_flow_flownode { get; set; }
//         public virtual DbSet<fa_flow_flownode_flow> fa_flow_flownode_flow { get; set; }
//         public virtual DbSet<fa_flow_flownode_role> fa_flow_flownode_role { get; set; }
//         public virtual DbSet<fa_function> fa_function { get; set; }
//         public virtual DbSet<fa_log> fa_log { get; set; }
//         public virtual DbSet<fa_login> fa_login { get; set; }
//         public virtual DbSet<fa_login_history> fa_login_history { get; set; }
//         public virtual DbSet<fa_message> fa_message { get; set; }
//         public virtual DbSet<fa_message_type> fa_message_type { get; set; }
//         public virtual DbSet<fa_module> fa_module { get; set; }
//         public virtual DbSet<fa_oauth> fa_oauth { get; set; }
//         public virtual DbSet<fa_oauth_login> fa_oauth_login { get; set; }
//         public virtual DbSet<fa_query> fa_query { get; set; }
//         public virtual DbSet<fa_role> fa_role { get; set; }
//         public virtual DbSet<fa_role_config> fa_role_config { get; set; }
//         public virtual DbSet<fa_role_function> fa_role_function { get; set; }
//         public virtual DbSet<fa_role_module> fa_role_module { get; set; }
//         public virtual DbSet<fa_role_query_authority> fa_role_query_authority { get; set; }
//         public virtual DbSet<fa_script> fa_script { get; set; }
//         public virtual DbSet<fa_script_group_list> fa_script_group_list { get; set; }
//         public virtual DbSet<fa_script_task> fa_script_task { get; set; }
//         public virtual DbSet<fa_script_task_log> fa_script_task_log { get; set; }
//         public virtual DbSet<fa_sms_send> fa_sms_send { get; set; }
//         public virtual DbSet<fa_task> fa_task { get; set; }
//         public virtual DbSet<fa_task_flow> fa_task_flow { get; set; }
//         public virtual DbSet<fa_task_flow_handle> fa_task_flow_handle { get; set; }
//         public virtual DbSet<fa_task_flow_handle_files> fa_task_flow_handle_files { get; set; }
//         public virtual DbSet<fa_task_flow_handle_user> fa_task_flow_handle_user { get; set; }
//         public virtual DbSet<fa_updata_log> fa_updata_log { get; set; }
//         public virtual DbSet<fa_user> fa_user { get; set; }
//         public virtual DbSet<fa_user_district> fa_user_district { get; set; }
//         public virtual DbSet<fa_user_event> fa_user_event { get; set; }
//         public virtual DbSet<fa_user_file> fa_user_file { get; set; }
//         public virtual DbSet<fa_user_friend> fa_user_friend { get; set; }
//         public virtual DbSet<fa_user_info> fa_user_info { get; set; }
//         public virtual DbSet<fa_user_role> fa_user_role { get; set; }

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
//             modelBuilder.Entity<fa_app_version>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.IS_NEW).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(1000)");

//                 entity.Property(e => e.TYPE)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.UPDATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.UPDATE_URL).HasColumnType("varchar(200)");
//             });

//             modelBuilder.Entity<fa_bulletin>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.AUTO_PEN).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.CONTENT).HasColumnType("text");

//                 entity.Property(e => e.CREATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.ISSUE_DATE).HasColumnType("datetime");

//                 entity.Property(e => e.IS_IMPORT).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IS_SHOW).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IS_URGENT).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.PIC).HasColumnType("varchar(255)");

//                 entity.Property(e => e.PUBLISHER)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.REGION)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.TITLE)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.TYPE_CODE).HasColumnType("varchar(50)");

//                 entity.Property(e => e.UPDATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<fa_bulletin_file>(entity =>
//             {
//                 entity.HasKey(e => new { e.BULLETIN_ID, e.FILE_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.FILE_ID)
//                     .HasName("FK_FA_BULLETIN_FILE_REF_FILE");

//                 entity.Property(e => e.BULLETIN_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FILE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.BULLETIN_)
//                     .WithMany(p => p.fa_bulletin_file)
//                     .HasForeignKey(d => d.BULLETIN_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_file_ibfk_1");

//                 entity.HasOne(d => d.FILE_)
//                     .WithMany(p => p.fa_bulletin_file)
//                     .HasForeignKey(d => d.FILE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_file_ibfk_2");
//             });

//             modelBuilder.Entity<fa_bulletin_log>(entity =>
//             {
//                 entity.HasIndex(e => e.BULLETIN_ID)
//                     .HasName("FK_BULLETIN_LOG_REF_BULLETIN");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.BULLETIN_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LOOK_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.BULLETIN_)
//                     .WithMany(p => p.fa_bulletin_log)
//                     .HasForeignKey(d => d.BULLETIN_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_log_ibfk_1");
//             });

//             modelBuilder.Entity<fa_bulletin_review>(entity =>
//             {
//                 entity.HasIndex(e => e.BULLETIN_ID)
//                     .HasName("FK_FA_BULLETIN_REVIEW_REF_BULL");

//                 entity.HasIndex(e => e.PARENT_ID)
//                     .HasName("FK_FA_BULLETIN_RE_REF_REVIEW");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ADD_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.BULLETIN_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CONTENT).HasColumnType("text");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.PARENT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.STATUS)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.STATUS_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.BULLETIN_)
//                     .WithMany(p => p.fa_bulletin_review)
//                     .HasForeignKey(d => d.BULLETIN_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_review_ibfk_1");

//                 entity.HasOne(d => d.PARENT_)
//                     .WithMany(p => p.InversePARENT_)
//                     .HasForeignKey(d => d.PARENT_ID)
//                     .HasConstraintName("fa_bulletin_review_ibfk_2");
//             });

//             modelBuilder.Entity<fa_bulletin_role>(entity =>
//             {
//                 entity.HasKey(e => new { e.BULLETIN_ID, e.ROLE_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.ROLE_ID)
//                     .HasName("FK_FA_BULLETIN_ROLE_REF_ROLE");

//                 entity.Property(e => e.BULLETIN_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.BULLETIN_)
//                     .WithMany(p => p.fa_bulletin_role)
//                     .HasForeignKey(d => d.BULLETIN_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_role_ibfk_1");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_bulletin_role)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_bulletin_role_ibfk_2");
//             });

//             modelBuilder.Entity<fa_bulletin_type>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(80)");
//             });

//             modelBuilder.Entity<fa_config>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ADD_TIEM).HasColumnType("datetime");

//                 entity.Property(e => e.ADD_USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CODE)
//                     .IsRequired()
//                     .HasColumnType("varchar(32)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.REGION)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");

//                 entity.Property(e => e.TYPE).HasColumnType("varchar(10)");

//                 entity.Property(e => e.VALUE).HasColumnType("varchar(300)");
//             });

//             modelBuilder.Entity<fa_db_server>(entity =>
//             {
//                 entity.HasIndex(e => e.DB_TYPE_ID)
//                     .HasName("FK_FA_DB_SERVER_REF_TYPE");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.DBNAME).HasColumnType("varchar(20)");

//                 entity.Property(e => e.DBUID)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.DB_LINK).HasColumnType("varchar(200)");

//                 entity.Property(e => e.DB_TYPE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.IP)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.NICKNAME).HasColumnType("varchar(32)");

//                 entity.Property(e => e.PASSWORD)
//                     .IsRequired()
//                     .HasColumnType("varchar(32)");

//                 entity.Property(e => e.PORT).HasColumnType("int(11)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");

//                 entity.Property(e => e.TO_PATH_D).HasColumnType("varchar(300)");

//                 entity.Property(e => e.TO_PATH_M).HasColumnType("varchar(300)");

//                 entity.Property(e => e.TYPE)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)");

//                 entity.HasOne(d => d.DB_TYPE_)
//                     .WithMany(p => p.fa_db_server)
//                     .HasForeignKey(d => d.DB_TYPE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_db_server_ibfk_1");
//             });

//             modelBuilder.Entity<fa_db_server_type>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(20)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");
//             });

//             modelBuilder.Entity<fa_district>(entity =>
//             {
//                 entity.HasIndex(e => e.PARENT_ID)
//                     .HasName("FK_FA_DISTRICT_REF_DISTRICT");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CODE).HasColumnType("varchar(50)");

//                 entity.Property(e => e.ID_PATH).HasColumnType("varchar(200)");

//                 entity.Property(e => e.IN_USE).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LEVEL_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.PARENT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.REGION)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)");

//                 entity.HasOne(d => d.PARENT_)
//                     .WithMany(p => p.InversePARENT_)
//                     .HasForeignKey(d => d.PARENT_ID)
//                     .HasConstraintName("fa_district_ibfk_1");
//             });

//             modelBuilder.Entity<fa_dynasty>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");
//             });

//             modelBuilder.Entity<fa_elder>(entity =>
//             {
//                 entity.HasIndex(e => e.FAMILY_ID)
//                     .HasName("FK_FA_ELDER_REF_FAMILY");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FAMILY_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(2)");

//                 entity.Property(e => e.SORT).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FAMILY_)
//                     .WithMany(p => p.fa_elder)
//                     .HasForeignKey(d => d.FAMILY_ID)
//                     .HasConstraintName("fa_elder_ibfk_1");
//             });

//             modelBuilder.Entity<fa_event_files>(entity =>
//             {
//                 entity.HasKey(e => new { e.EVENT_ID, e.FILES_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.FILES_ID)
//                     .HasName("FK_FA_EVENT_FILES_REF_FILES");

//                 entity.Property(e => e.EVENT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FILES_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.EVENT_)
//                     .WithMany(p => p.fa_event_files)
//                     .HasForeignKey(d => d.EVENT_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_event_files_ibfk_1");

//                 entity.HasOne(d => d.FILES_)
//                     .WithMany(p => p.fa_event_files)
//                     .HasForeignKey(d => d.FILES_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_event_files_ibfk_2");
//             });

//             modelBuilder.Entity<fa_export_log>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.EXPORT_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.LOGIN_NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(100)");

//                 entity.Property(e => e.SQL_CONTENT).HasColumnType("text");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<fa_family>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");
//             });

//             modelBuilder.Entity<fa_files>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FILE_TYPE).HasColumnType("varchar(50)");

//                 entity.Property(e => e.LENGTH).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.PATH)
//                     .IsRequired()
//                     .HasColumnType("varchar(200)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(2000)");

//                 entity.Property(e => e.UPLOAD_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.URL).HasColumnType("varchar(254)");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<fa_flow>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FLOW_TYPE)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.REGION).HasColumnType("varchar(10)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(100)");

//                 entity.Property(e => e.X_Y).HasColumnType("varchar(500)");
//             });

//             modelBuilder.Entity<fa_flow_flownode>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.HANDLE_URL).HasColumnType("varchar(200)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.SHOW_URL).HasColumnType("varchar(200)");
//             });

//             modelBuilder.Entity<fa_flow_flownode_flow>(entity =>
//             {
//                 entity.HasIndex(e => e.FLOW_ID)
//                     .HasName("FK_FA_FLOWNODE_FLOW_REF_FLOW");

//                 entity.HasIndex(e => e.FROM_FLOWNODE_ID)
//                     .HasName("FK_FA_FLOWNODE_FLOW_REF_NODE");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ASSIGNER).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.EXPIRE_HOUR).HasColumnType("int(11)");

//                 entity.Property(e => e.FLOW_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FROM_FLOWNODE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.HANDLE).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(20)");

//                 entity.Property(e => e.STATUS).HasColumnType("varchar(20)");

//                 entity.Property(e => e.TO_FLOWNODE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FLOW_)
//                     .WithMany(p => p.fa_flow_flownode_flow)
//                     .HasForeignKey(d => d.FLOW_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_flow_ibfk_1");

//                 entity.HasOne(d => d.FROM_FLOWNODE_)
//                     .WithMany(p => p.fa_flow_flownode_flow)
//                     .HasForeignKey(d => d.FROM_FLOWNODE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_flow_ibfk_2");
//             });

//             modelBuilder.Entity<fa_flow_flownode_role>(entity =>
//             {
//                 entity.HasKey(e => new { e.FLOW_ID, e.ROLE_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.ROLE_ID)
//                     .HasName("FK_FA_FLOW_REF_ROLE");

//                 entity.Property(e => e.FLOW_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FLOW_)
//                     .WithMany(p => p.fa_flow_flownode_role)
//                     .HasForeignKey(d => d.FLOW_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_role_ibfk_2");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_flow_flownode_role)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_flow_flownode_role_ibfk_1");
//             });

//             modelBuilder.Entity<fa_function>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CLASS_NAME).HasColumnType("varchar(100)");

//                 entity.Property(e => e.DLL_NAME).HasColumnType("varchar(100)");

//                 entity.Property(e => e.FULL_NAME).HasColumnType("varchar(100)");

//                 entity.Property(e => e.METHOD_NAME).HasColumnType("varchar(100)");

//                 entity.Property(e => e.NAMESPACE).HasColumnType("varchar(100)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(100)");

//                 entity.Property(e => e.XML_NOTE).HasColumnType("varchar(254)");
//             });

//             modelBuilder.Entity<fa_log>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ADD_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.MODULE_NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(100)");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<fa_login>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.EMAIL_ADDR).HasColumnType("varchar(255)");

//                 entity.Property(e => e.FAIL_COUNT).HasColumnType("int(11)");

//                 entity.Property(e => e.IS_LOCKED).HasColumnType("int(1)");

//                 entity.Property(e => e.LOCKED_REASON).HasColumnType("varchar(255)");

//                 entity.Property(e => e.LOGIN_NAME).HasColumnType("varchar(20)");

//                 entity.Property(e => e.PASSWORD).HasColumnType("varchar(255)");

//                 entity.Property(e => e.PASS_UPDATE_DATE).HasColumnType("datetime");

//                 entity.Property(e => e.PHONE_NO).HasColumnType("varchar(20)");

//                 entity.Property(e => e.VERIFY_CODE).HasColumnType("varchar(10)");

//                 entity.Property(e => e.VERIFY_TIME).HasColumnType("datetime");
//             });

//             modelBuilder.Entity<fa_login_history>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LOGIN_HISTORY_TYPE).HasColumnType("int(11)");

//                 entity.Property(e => e.LOGIN_HOST).HasColumnType("varchar(255)");

//                 entity.Property(e => e.LOGIN_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.LOGOUT_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.MESSAGE).HasColumnType("varchar(255)");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<fa_message>(entity =>
//             {
//                 entity.HasIndex(e => e.MESSAGE_TYPE_ID)
//                     .HasName("FK_FA_MESSAGE_REF_MESSAGE_TYPE");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ALL_ROLE_ID).HasColumnType("varchar(500)");

//                 entity.Property(e => e.CONTENT).HasColumnType("varchar(500)");

//                 entity.Property(e => e.CREATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.CREATE_USERID).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_USERNAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.DISTRICT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.KEY_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.MESSAGE_TYPE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.PUSH_TYPE).HasColumnType("varchar(10)");

//                 entity.Property(e => e.STATUS).HasColumnType("varchar(10)");

//                 entity.Property(e => e.TITLE).HasColumnType("varchar(100)");

//                 entity.HasOne(d => d.MESSAGE_TYPE_)
//                     .WithMany(p => p.fa_message)
//                     .HasForeignKey(d => d.MESSAGE_TYPE_ID)
//                     .HasConstraintName("fa_message_ibfk_1");
//             });

//             modelBuilder.Entity<fa_message_type>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.IS_USE).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");

//                 entity.Property(e => e.TABLE_NAME).HasColumnType("varchar(50)");
//             });

//             modelBuilder.Entity<fa_module>(entity =>
//             {
//                 entity.HasIndex(e => e.PARENT_ID)
//                     .HasName("FK_FA_MODULE_REF_MODULE");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CODE).HasColumnType("varchar(20)");

//                 entity.Property(e => e.DESCRIPTION).HasColumnType("varchar(2000)");

//                 entity.Property(e => e.DESKTOP_ROLE).HasColumnType("varchar(200)");

//                 entity.Property(e => e.H).HasColumnType("int(11)");

//                 entity.Property(e => e.IMAGE_URL).HasColumnType("varchar(2000)");

//                 entity.Property(e => e.IS_DEBUG).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.IS_HIDE).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LOCATION).HasColumnType("varchar(2000)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(60)");

//                 entity.Property(e => e.PARENT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.SHOW_ORDER).HasColumnType("decimal(2,0)");

//                 entity.Property(e => e.W).HasColumnType("int(11)");

//                 entity.HasOne(d => d.PARENT_)
//                     .WithMany(p => p.InversePARENT_)
//                     .HasForeignKey(d => d.PARENT_ID)
//                     .HasConstraintName("fa_module_ibfk_1");
//             });

//             modelBuilder.Entity<fa_oauth>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LOGIN_URL).HasColumnType("varchar(500)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.REG_URL).HasColumnType("varchar(500)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");
//             });

//             modelBuilder.Entity<fa_oauth_login>(entity =>
//             {
//                 entity.HasKey(e => new { e.OAUTH_ID, e.LOGIN_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.LOGIN_ID)
//                     .HasName("FK_FA_OAUTH_REF_LOGIN");

//                 entity.Property(e => e.OAUTH_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LOGIN_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.LOGIN_)
//                     .WithMany(p => p.fa_oauth_login)
//                     .HasForeignKey(d => d.LOGIN_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_oauth_login_ibfk_1");

//                 entity.HasOne(d => d.OAUTH_)
//                     .WithMany(p => p.fa_oauth_login)
//                     .HasForeignKey(d => d.OAUTH_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_oauth_login_ibfk_2");
//             });

//             modelBuilder.Entity<fa_query>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.AUTO_LOAD).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.CHARTS_CFG).HasColumnType("text");

//                 entity.Property(e => e.CHARTS_TYPE).HasColumnType("varchar(50)");

//                 entity.Property(e => e.CODE)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.DB_SERVER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FILTR_LEVEL).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.FILTR_STR).HasColumnType("text");

//                 entity.Property(e => e.HEARD_BTN).HasColumnType("text");

//                 entity.Property(e => e.IN_PARA_JSON).HasColumnType("text");

//                 entity.Property(e => e.IS_DEBUG).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.JS_STR).HasColumnType("text");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.NEW_DATA).HasColumnType("varchar(50)");

//                 entity.Property(e => e.PAGE_SIZE).HasColumnType("int(11)");

//                 entity.Property(e => e.QUERY_CFG_JSON).HasColumnType("text");

//                 entity.Property(e => e.QUERY_CONF).HasColumnType("text");

//                 entity.Property(e => e.REMARK).HasColumnType("text");

//                 entity.Property(e => e.REPORT_SCRIPT).HasColumnType("text");

//                 entity.Property(e => e.ROWS_BTN).HasColumnType("text");

//                 entity.Property(e => e.SHOW_CHECKBOX).HasColumnType("decimal(1,0)");
//             });

//             modelBuilder.Entity<fa_role>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(80)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(255)");

//                 entity.Property(e => e.TYPE).HasColumnType("int(11)");
//             });

//             modelBuilder.Entity<fa_role_config>(entity =>
//             {
//                 entity.HasIndex(e => e.ROLE_ID)
//                     .HasName("FK_FA_ROLE_CONFIG_REF_ROLE");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.TYPE).HasColumnType("varchar(10)");

//                 entity.Property(e => e.VALUE).HasColumnType("varchar(300)");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_role_config)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_config_ibfk_1");
//             });

//             modelBuilder.Entity<fa_role_function>(entity =>
//             {
//                 entity.HasKey(e => new { e.FUNCTION_ID, e.ROLE_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.ROLE_ID)
//                     .HasName("FK_FA_ROLE_FUNCTION_REF_ROLE");

//                 entity.Property(e => e.FUNCTION_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FUNCTION_)
//                     .WithMany(p => p.fa_role_function)
//                     .HasForeignKey(d => d.FUNCTION_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_function_ibfk_1");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_role_function)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_function_ibfk_2");
//             });

//             modelBuilder.Entity<fa_role_module>(entity =>
//             {
//                 entity.HasKey(e => new { e.ROLE_ID, e.MODULE_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.MODULE_ID)
//                     .HasName("FK_FA_ROLE_MODULE_REF_MODULE");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.MODULE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.MODULE_)
//                     .WithMany(p => p.fa_role_module)
//                     .HasForeignKey(d => d.MODULE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_module_ibfk_1");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_role_module)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_module_ibfk_2");
//             });

//             modelBuilder.Entity<fa_role_query_authority>(entity =>
//             {
//                 entity.HasKey(e => new { e.ROLE_ID, e.QUERY_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.QUERY_ID)
//                     .HasName("FK_FA_ROLE_QUERY_REF_QUERY");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.QUERY_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NO_AUTHORITY).HasColumnType("varchar(200)");

//                 entity.HasOne(d => d.QUERY_)
//                     .WithMany(p => p.fa_role_query_authority)
//                     .HasForeignKey(d => d.QUERY_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_query_authority_ibfk_1");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_role_query_authority)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_role_query_authority_ibfk_2");
//             });

//             modelBuilder.Entity<fa_script>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.BODY_HASH)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.BODY_TEXT)
//                     .IsRequired()
//                     .HasColumnType("text");

//                 entity.Property(e => e.CODE)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)");

//                 entity.Property(e => e.DISABLE_REASON).HasColumnType("varchar(50)");

//                 entity.Property(e => e.IS_GROUP).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.REGION).HasColumnType("varchar(10)");

//                 entity.Property(e => e.RUN_ARGS).HasColumnType("varchar(255)");

//                 entity.Property(e => e.RUN_DATA)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RUN_WHEN).HasColumnType("varchar(30)");

//                 entity.Property(e => e.SERVICE_FLAG).HasColumnType("varchar(50)");

//                 entity.Property(e => e.STATUS).HasColumnType("varchar(10)");
//             });

//             modelBuilder.Entity<fa_script_group_list>(entity =>
//             {
//                 entity.HasKey(e => new { e.SCRIPT_ID, e.GROUP_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.GROUP_ID)
//                     .HasName("FK_FA_GROUP_LIST_REF_SCRIPT");

//                 entity.Property(e => e.SCRIPT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.GROUP_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ORDER_INDEX).HasColumnType("int(11)");

//                 entity.HasOne(d => d.GROUP_)
//                     .WithMany(p => p.fa_script_group_list)
//                     .HasForeignKey(d => d.GROUP_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_script_group_list_ibfk_1");
//             });

//             modelBuilder.Entity<fa_script_task>(entity =>
//             {
//                 entity.HasIndex(e => e.SCRIPT_ID)
//                     .HasName("FK_FA_SCRIPT_TASK_REF_SCRIPT");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.BODY_HASH)
//                     .IsRequired()
//                     .HasColumnType("varchar(255)");

//                 entity.Property(e => e.BODY_TEXT)
//                     .IsRequired()
//                     .HasColumnType("text");

//                 entity.Property(e => e.DISABLE_DATE).HasColumnType("datetime");

//                 entity.Property(e => e.DISABLE_REASON).HasColumnType("varchar(50)");

//                 entity.Property(e => e.DSL_TYPE).HasColumnType("varchar(255)");

//                 entity.Property(e => e.END_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.GROUP_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LOG_TYPE)
//                     .HasColumnType("decimal(1,0)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.REGION).HasColumnType("varchar(10)");

//                 entity.Property(e => e.RETURN_CODE)
//                     .HasColumnType("varchar(10)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RUN_ARGS).HasColumnType("varchar(255)");

//                 entity.Property(e => e.RUN_DATA)
//                     .IsRequired()
//                     .HasColumnType("varchar(20)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RUN_STATE)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)")
//                     .HasDefaultValueSql("'0'");

//                 entity.Property(e => e.RUN_WHEN).HasColumnType("varchar(30)");

//                 entity.Property(e => e.SCRIPT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.SERVICE_FLAG).HasColumnType("varchar(50)");

//                 entity.Property(e => e.START_TIME).HasColumnType("datetime");

//                 entity.HasOne(d => d.SCRIPT_)
//                     .WithMany(p => p.fa_script_task)
//                     .HasForeignKey(d => d.SCRIPT_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_script_task_ibfk_1");
//             });

//             modelBuilder.Entity<fa_script_task_log>(entity =>
//             {
//                 entity.HasIndex(e => e.SCRIPT_TASK_ID)
//                     .HasName("FK_FA_SCRIPT_TASK_LOG_REF_TASK");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LOG_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.LOG_TYPE)
//                     .HasColumnType("decimal(1,0)")
//                     .HasDefaultValueSql("'1'");

//                 entity.Property(e => e.MESSAGE).HasColumnType("text");

//                 entity.Property(e => e.SCRIPT_TASK_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.SQL_TEXT).HasColumnType("text");

//                 entity.HasOne(d => d.SCRIPT_TASK_)
//                     .WithMany(p => p.fa_script_task_log)
//                     .HasForeignKey(d => d.SCRIPT_TASK_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_script_task_log_ibfk_1");
//             });

//             modelBuilder.Entity<fa_sms_send>(entity =>
//             {
//                 entity.HasKey(e => e.GUID)
//                     .HasName("PRIMARY");

//                 entity.Property(e => e.GUID).HasColumnType("char(32)");

//                 entity.Property(e => e.ADD_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.CONTENT)
//                     .IsRequired()
//                     .HasColumnType("varchar(500)");

//                 entity.Property(e => e.MESSAGE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.PHONE_NO)
//                     .IsRequired()
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.SEND_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.STAUTS).HasColumnType("varchar(15)");

//                 entity.Property(e => e.TRY_NUM)
//                     .HasColumnType("int(11)")
//                     .HasDefaultValueSql("'0'");
//             });

//             modelBuilder.Entity<fa_task>(entity =>
//             {
//                 entity.HasIndex(e => e.FLOW_ID)
//                     .HasName("FK_FA_FLOW_TASK_REF_FLOW");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.CREATE_USER).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_USER_NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.DEAL_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.END_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.FLOW_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.KEY_ID).HasColumnType("varchar(32)");

//                 entity.Property(e => e.REGION).HasColumnType("varchar(10)");

//                 entity.Property(e => e.REMARK).HasColumnType("text");

//                 entity.Property(e => e.ROLE_ID_STR).HasColumnType("varchar(200)");

//                 entity.Property(e => e.START_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.STATUS).HasColumnType("varchar(50)");

//                 entity.Property(e => e.STATUS_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.TASK_NAME).HasColumnType("varchar(50)");

//                 entity.HasOne(d => d.FLOW_)
//                     .WithMany(p => p.fa_task)
//                     .HasForeignKey(d => d.FLOW_ID)
//                     .HasConstraintName("fa_task_ibfk_1");
//             });

//             modelBuilder.Entity<fa_task_flow>(entity =>
//             {
//                 entity.HasIndex(e => e.PARENT_ID)
//                     .HasName("FK_FA_TASK_FLOW_REF_TASK_FLOW");

//                 entity.HasIndex(e => e.TASK_ID)
//                     .HasName("FK_FA_TASK_FLOW_REF_TASK");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ACCEPT_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.DEAL_STATUS).HasColumnType("varchar(50)");

//                 entity.Property(e => e.DEAL_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.EQUAL_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.EXPIRE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.FLOWNODE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.HANDLE_URL).HasColumnType("varchar(200)");

//                 entity.Property(e => e.HANDLE_USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.IS_HANDLE).HasColumnType("int(11)");

//                 entity.Property(e => e.LEVEL_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(100)");

//                 entity.Property(e => e.PARENT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ROLE_ID_STR).HasColumnType("varchar(200)");

//                 entity.Property(e => e.SHOW_URL).HasColumnType("varchar(200)");

//                 entity.Property(e => e.START_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.TASK_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.PARENT_)
//                     .WithMany(p => p.InversePARENT_)
//                     .HasForeignKey(d => d.PARENT_ID)
//                     .HasConstraintName("fa_task_flow_ibfk_2");

//                 entity.HasOne(d => d.TASK_)
//                     .WithMany(p => p.fa_task_flow)
//                     .HasForeignKey(d => d.TASK_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_ibfk_1");
//             });

//             modelBuilder.Entity<fa_task_flow_handle>(entity =>
//             {
//                 entity.HasIndex(e => e.TASK_FLOW_ID)
//                     .HasName("FK_TASK_FLOW_HANDLE_REF_FLOW");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CONTENT)
//                     .IsRequired()
//                     .HasColumnType("varchar(2000)");

//                 entity.Property(e => e.DEAL_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.DEAL_USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.DEAL_USER_NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(50)");

//                 entity.Property(e => e.TASK_FLOW_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.TASK_FLOW_)
//                     .WithMany(p => p.fa_task_flow_handle)
//                     .HasForeignKey(d => d.TASK_FLOW_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_ibfk_1");
//             });

//             modelBuilder.Entity<fa_task_flow_handle_files>(entity =>
//             {
//                 entity.HasKey(e => new { e.FLOW_HANDLE_ID, e.FILES_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.FILES_ID)
//                     .HasName("FK_FLOW_HANDLE_REF_FILES");

//                 entity.Property(e => e.FLOW_HANDLE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FILES_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FILES_)
//                     .WithMany(p => p.fa_task_flow_handle_files)
//                     .HasForeignKey(d => d.FILES_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_files_ibfk_1");

//                 entity.HasOne(d => d.FLOW_HANDLE_)
//                     .WithMany(p => p.fa_task_flow_handle_files)
//                     .HasForeignKey(d => d.FLOW_HANDLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_files_ibfk_2");
//             });

//             modelBuilder.Entity<fa_task_flow_handle_user>(entity =>
//             {
//                 entity.HasKey(e => new { e.TASK_FLOW_ID, e.HANDLE_USER_ID })
//                     .HasName("PRIMARY");

//                 entity.Property(e => e.TASK_FLOW_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.HANDLE_USER_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.TASK_FLOW_)
//                     .WithMany(p => p.fa_task_flow_handle_user)
//                     .HasForeignKey(d => d.TASK_FLOW_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_task_flow_handle_user_ibfk_1");
//             });

//             modelBuilder.Entity<fa_updata_log>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.CREATE_USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_USER_NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.NEW_CONTENT).HasColumnType("text");

//                 entity.Property(e => e.OLD_CONTENT).HasColumnType("text");

//                 entity.Property(e => e.TABLE_NAME).HasColumnType("varchar(50)");
//             });

//             modelBuilder.Entity<fa_user>(entity =>
//             {
//                 entity.HasIndex(e => e.DISTRICT_ID)
//                     .HasName("FK_FA_USER_REF_DISTRICT");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.DISTRICT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ICON_FILES_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.IS_LOCKED).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LAST_ACTIVE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.LAST_LOGIN_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.LAST_LOGOUT_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.LOGIN_COUNT).HasColumnType("int(11)");

//                 entity.Property(e => e.LOGIN_NAME).HasColumnType("varchar(20)");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(80)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(2000)");

//                 entity.HasOne(d => d.DISTRICT_)
//                     .WithMany(p => p.fa_user)
//                     .HasForeignKey(d => d.DISTRICT_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_ibfk_1");
//             });

//             modelBuilder.Entity<fa_user_district>(entity =>
//             {
//                 entity.HasKey(e => new { e.USER_ID, e.DISTRICT_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.DISTRICT_ID)
//                     .HasName("FK_FA_USER_DISTRICT_REF_DIST");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.DISTRICT_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.DISTRICT_)
//                     .WithMany(p => p.fa_user_district)
//                     .HasForeignKey(d => d.DISTRICT_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_district_ibfk_1");

//                 entity.HasOne(d => d.USER_)
//                     .WithMany(p => p.fa_user_district)
//                     .HasForeignKey(d => d.USER_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_district_ibfk_2");
//             });

//             modelBuilder.Entity<fa_user_event>(entity =>
//             {
//                 entity.HasIndex(e => e.USER_ID)
//                     .HasName("FK_FA_USER_EVENT_REF_USER");

//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ADDRESS).HasColumnType("varchar(500)");

//                 entity.Property(e => e.CONTENT).HasColumnType("varchar(500)");

//                 entity.Property(e => e.HAPPEN_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.NAME).HasColumnType("varchar(50)");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.USER_)
//                     .WithMany(p => p.fa_user_event)
//                     .HasForeignKey(d => d.USER_ID)
//                     .HasConstraintName("fa_user_event_ibfk_1");
//             });

//             modelBuilder.Entity<fa_user_file>(entity =>
//             {
//                 entity.HasKey(e => new { e.USER_ID, e.FILE_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.FILE_ID)
//                     .HasName("FK_FA_USER_FILE_REF_FILE");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FILE_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FILE_)
//                     .WithMany(p => p.fa_user_file)
//                     .HasForeignKey(d => d.FILE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_file_ibfk_1");

//                 entity.HasOne(d => d.USER_)
//                     .WithMany(p => p.fa_user_file)
//                     .HasForeignKey(d => d.USER_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_file_ibfk_2");
//             });

//             modelBuilder.Entity<fa_user_friend>(entity =>
//             {
//                 entity.HasKey(e => new { e.USER_ID, e.FRIEND_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.FRIEND_ID)
//                     .HasName("FK_FA_FRIEND_REF_USER");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FRIEND_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.FRIEND_)
//                     .WithMany(p => p.fa_user_friend)
//                     .HasForeignKey(d => d.FRIEND_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_friend_ibfk_1");

//                 entity.HasOne(d => d.USER_)
//                     .WithMany(p => p.fa_user_friend)
//                     .HasForeignKey(d => d.USER_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("fa_user_friend_ibfk_2");
//             });

//             modelBuilder.Entity<fa_user_info>(entity =>
//             {
//                 entity.Property(e => e.ID).HasColumnType("int(11)");

//                 entity.Property(e => e.ALIAS).HasColumnType("varchar(10)");

//                 entity.Property(e => e.AUTHORITY).HasColumnType("int(11)");

//                 entity.Property(e => e.BIRTHDAY_PLACE).HasColumnType("varchar(500)");

//                 entity.Property(e => e.BIRTHDAY_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.CONSORT_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.COUPLE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.CREATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.CREATE_USER_ID)
//                     .HasColumnType("int(11)")
//                     .HasDefaultValueSql("'1'");

//                 entity.Property(e => e.CREATE_USER_NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(50)")
//                     .HasDefaultValueSql("'admin'");

//                 entity.Property(e => e.DIED_PLACE).HasColumnType("varchar(500)");

//                 entity.Property(e => e.DIED_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.ELDER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FAMILY_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.FATHER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.IS_LIVE).HasColumnType("decimal(1,0)");

//                 entity.Property(e => e.LEVEL_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.LEVEL_NAME).HasColumnType("varchar(2)");

//                 entity.Property(e => e.MOTHER_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.REMARK).HasColumnType("varchar(500)");

//                 entity.Property(e => e.SEX).HasColumnType("varchar(2)");

//                 entity.Property(e => e.STATUS)
//                     .IsRequired()
//                     .HasColumnType("varchar(10)")
//                     .HasDefaultValueSql("'正常'");

//                 entity.Property(e => e.UPDATE_TIME).HasColumnType("datetime");

//                 entity.Property(e => e.UPDATE_USER_ID)
//                     .HasColumnType("int(11)")
//                     .HasDefaultValueSql("'1'");

//                 entity.Property(e => e.UPDATE_USER_NAME)
//                     .IsRequired()
//                     .HasColumnType("varchar(50)")
//                     .HasDefaultValueSql("'admin'");

//                 entity.Property(e => e.YEARS_TYPE).HasColumnType("varchar(10)");
//             });

//             modelBuilder.Entity<fa_user_role>(entity =>
//             {
//                 entity.HasKey(e => new { e.ROLE_ID, e.USER_ID })
//                     .HasName("PRIMARY");

//                 entity.HasIndex(e => e.USER_ID)
//                     .HasName("FK_FA_USER_ROLE_REF_USER");

//                 entity.Property(e => e.ROLE_ID).HasColumnType("int(11)");

//                 entity.Property(e => e.USER_ID).HasColumnType("int(11)");

//                 entity.HasOne(d => d.ROLE_)
//                     .WithMany(p => p.fa_user_role)
//                     .HasForeignKey(d => d.ROLE_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("FK_FA_USER_ROLE_REF_ROLE");

//                 entity.HasOne(d => d.USER_)
//                     .WithMany(p => p.fa_user_role)
//                     .HasForeignKey(d => d.USER_ID)
//                     .OnDelete(DeleteBehavior.ClientSetNull)
//                     .HasConstraintName("FK_FA_USER_ROLE_REF_USER");
//             });
//         }
//     }
// }
