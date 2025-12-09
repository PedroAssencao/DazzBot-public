using Chatbot.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.API.DAL
{

    public partial class chatbotContext : DbContext
    {
        public chatbotContext()
        {
        }

        public chatbotContext(DbContextOptions<chatbotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Atendente> Atendentes { get; set; }

        public virtual DbSet<Atendimento> Atendimentos { get; set; }

        public virtual DbSet<Chat> Chats { get; set; }

        public virtual DbSet<Contato> Contatos { get; set; }

        public virtual DbSet<Departamento> Departamentos { get; set; }

        public virtual DbSet<Login> Logins { get; set; }

        public virtual DbSet<Mensagen> Mensagens { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<Option> Options { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-02BUU56;Initial Catalog=chatbot;Integrated Security=True;Encrypt=False");
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Chinook");
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseSqlServer("Data Source=LAPTOP-M68K5TBC\\SQLEXPRESS;Initial Catalog=chatbot;Integrated Security=True;Encrypt=False");
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseSqlServer("Data Source=LAPTOP-M68K5TBC\\SQLEXPRESS;Initial Catalog=chatbot;Integrated Security=True;Encrypt=False");
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Data Source=.\\SENAI2023;Initial Catalog=chatbot;User ID=sa;Password=senai.123;Encrypt=False");
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseSqlServer("Data Source=SQL8006.site4now.net;Initial Catalog=db_a964fc_chatbot;User Id=db_a964fc_chatbot_admin;Password=senai.123");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendente>(entity =>
            {
                entity.HasKey(e => e.AteId).HasName("PK__atendent__895194D674414920");

                entity.HasOne(d => d.Dep).WithMany(p => p.Atendentes).HasConstraintName("FK__atendente__dep_i__403A8C7D").OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Log).WithMany(p => p.Atendentes).HasConstraintName("FK__atendente__log_i__3F466844").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Atendimento>(entity =>
            {
                entity.HasKey(e => e.AtenId).HasName("PK__Atendime__F4B66A4080C48752");

                entity.HasOne(d => d.Ate).WithMany(p => p.Atendimentos).HasConstraintName("FK__Atendimen__ate_i__4316F928").OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Con).WithMany(p => p.Atendimentos).HasConstraintName("FK__Atendimen__con_i__44FF419A").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Dep).WithMany(p => p.Atendimentos).HasConstraintName("FK__Atendimen__dep_i__440B1D61").OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Log).WithMany(p => p.Atendimentos).HasConstraintName("FK__Atendimen__log_i__45F365D3").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.ChaId).HasName("PK__chat__5AF8FDEA92B3BDD0");

                entity.HasOne(d => d.Ate).WithMany(p => p.Chats).HasConstraintName("FK__chat__ate_id__48CFD27E").OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Aten).WithMany(p => p.Chats).HasConstraintName("FK__chat__aten_id__4BAC3F29").OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Con).WithMany(p => p.Chats).HasConstraintName("FK__chat__con_id__4AB81AF0").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Log).WithMany(p => p.Chats).HasConstraintName("FK__chat__log_id__49C3F6B7").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Contato>(entity =>
            {
                entity.HasKey(e => e.ConId).HasName("PK__contatos__081B0F1A9391D70E");

                entity.HasOne(d => d.Log).WithMany(p => p.Contatos).HasConstraintName("FK__contatos__log_id__398D8EEE").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.DepId).HasName("PK__departam__BB4BD8F8D55B4951");

                entity.HasOne(d => d.Log).WithMany(p => p.Departamentos).HasConstraintName("FK__departame__log_i__3C69FB99").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.LogId).HasName("PK__login__9E2397E023C6542C");
            });

            modelBuilder.Entity<Mensagen>(entity =>
            {
                entity.HasKey(e => e.MensId).HasName("PK__Mensagen__763E9E0AC3136BFE");

                entity.HasOne(d => d.Cha).WithMany(p => p.Mensagens).HasConstraintName("FK__Mensagens__cha_i__5070F446").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Con).WithMany(p => p.Mensagens).HasConstraintName("FK__Mensagens__con_i__4E88ABD4").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Log).WithMany(p => p.Mensagens).HasConstraintName("FK__Mensagens__log_i__4F7CD00D").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenId).HasName("PK__menus__387DDE002DE1152B");

                entity.HasOne(d => d.Log).WithMany(p => p.Menus).HasConstraintName("FK__menus__log_id__534D60F1").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.OptId).HasName("PK__options__84DB9F9B4CD6FC31");

                entity.HasOne(d => d.Log).WithMany(p => p.Options).HasConstraintName("FK__options__log_id__5629CD9C").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Men).WithMany(p => p.Options).HasConstraintName("FK__options__men_id__571DF1D5").OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
