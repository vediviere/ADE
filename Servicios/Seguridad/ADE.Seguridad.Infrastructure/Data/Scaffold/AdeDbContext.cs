using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

// 🐾 CAMINO DE MIGAJAS -- 5° QUINTA PARADA 🐾 Aquí está el puente con la base de datos. 🐾

//Este contexto fue generado desde la BD de "ADE"
//Representa las tablas del esquema "adedb"
//Este contexto lo usa el repositorio para acceder a SQL Server

// 🐾 CONTINUAMOS A LA SEXTA PARADA => JwtService 🐾

public partial class AdeDbContext : DbContext
{
    // 🐾🐾 Constructor que recibe opciones de configuración para el contexto 🐾🐾
    public AdeDbContext(DbContextOptions<AdeDbContext> options)
        : base(options)
    {
    }

    // 🐾🐾 Representación de las tablas de la base de datos como DbSet<T> 🐾🐾
    public virtual DbSet<actividad_apoyo> actividad_apoyos { get; set; }

    public virtual DbSet<apoyo_docencium> apoyo_docencia { get; set; }

    public virtual DbSet<asignatura> asignaturas { get; set; }

    public virtual DbSet<aula> aulas { get; set; }

    public virtual DbSet<carga_academica> carga_academicas { get; set; }

    public virtual DbSet<carrera> carreras { get; set; }

    public virtual DbSet<correos_recuperacion> correos_recuperacions { get; set; }

    public virtual DbSet<detallesadministrativo> detallesadministrativos { get; set; }

    public virtual DbSet<detallesdocente> detallesdocentes { get; set; }

    public virtual DbSet<detallesestudiante> detallesestudiantes { get; set; }

    public virtual DbSet<detallesjefatura> detallesjefaturas { get; set; }

    public virtual DbSet<fechas_carga> fechas_cargas { get; set; }

    public virtual DbSet<grupo> grupos { get; set; }

    public virtual DbSet<horario> horarios { get; set; }

    public virtual DbSet<horario_apoyo> horario_apoyos { get; set; }

    public virtual DbSet<horario_tutoria> horario_tutorias { get; set; }

    public virtual DbSet<materias_asignada> materias_asignadas { get; set; }

    public virtual DbSet<materias_cursar> materias_cursars { get; set; }

    public virtual DbSet<media_superior> media_superiors { get; set; }

    public virtual DbSet<nombremediasuperior> nombremediasuperiors { get; set; }

    public virtual DbSet<notificacione> notificaciones { get; set; }

    public virtual DbSet<pago> pagos { get; set; }

    public virtual DbSet<persona> personas { get; set; }
    public virtual DbSet<recuperacion> recuperacions { get; set; }

    public virtual DbSet<roles_usuario> roles_usuarios { get; set; }

    public virtual DbSet<solicitud_inscripcion> solicitud_inscripcions { get; set; }

    public virtual DbSet<solicitud_re_inscripcion> solicitud_re_inscripcions { get; set; }

    public virtual DbSet<tutor> tutors { get; set; }

    public virtual DbSet<tutorgrupo> tutorgrupos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<actividad_apoyo>(entity =>
        {
            entity.HasKey(e => e.id_actividad_apoyo).HasName("PK_actividad_apoyo_id_actividad_apoyo");

            entity.ToTable("actividad_apoyo", "adedb");

            entity.Property(e => e.nombre_actividad).HasMaxLength(50);
        });

        modelBuilder.Entity<apoyo_docencium>(entity =>
        {
            entity.HasKey(e => e.id_apoyo_docencia).HasName("PK_apoyo_docencia_id_apoyo_docencia");

            entity.ToTable("apoyo_docencia", "adedb");

            entity.HasIndex(e => e.id_carga_academica, "FK_apoyo_docencia_ca");

            entity.HasIndex(e => e.id_actividad_apoyo, "id_actividad_apoyo");

            entity.Property(e => e.autotimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.horas).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_carga_academica).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.observaciones)
                .HasMaxLength(500)
                .HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_actividad_apoyoNavigation).WithMany(p => p.apoyo_docencia)
                .HasForeignKey(d => d.id_actividad_apoyo)
                .HasConstraintName("apoyo_docencia$apoyo_docencia_ibfk_1");

            entity.HasOne(d => d.id_carga_academicaNavigation).WithMany(p => p.apoyo_docencia)
                .HasForeignKey(d => d.id_carga_academica)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apoyo_docencia$apoyo_docencia_ibfk_2");
        });

        modelBuilder.Entity<asignatura>(entity =>
        {
            entity.HasKey(e => e.id_asignatura).HasName("PK_asignatura_id_asignatura");

            entity.ToTable("asignatura", "adedb");

            entity.HasIndex(e => e.id_carrera, "FK_asignatura_persona");

            entity.Property(e => e.autotimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.clv_asignatura)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.creditos).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.h_practicas).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.h_teoricas).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.nombreMat)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.semestremateria)
                .HasMaxLength(2)
                .HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_carreraNavigation).WithMany(p => p.asignaturas)
                .HasForeignKey(d => d.id_carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignatura$FK_asignatura_persona");
        });

        modelBuilder.Entity<aula>(entity =>
        {
            entity.HasKey(e => e.id_aula).HasName("PK_aula_id_aula");

            entity.ToTable("aula", "adedb");

            entity.Property(e => e.nombre_aula).HasMaxLength(7);
        });

        modelBuilder.Entity<carga_academica>(entity =>
        {
            entity.HasKey(e => e.id_carga_academica).HasName("PK_carga_academica_id_carga_academica");

            entity.ToTable("carga_academica", "adedb");

            entity.HasIndex(e => e.id_fechas_carga, "FK_carga_academica_fechas_carga");

            entity.HasIndex(e => e.id_docente, "FK_carga_cademica_detallesdocente");

            entity.HasIndex(e => e.id_jefatura, "FK_carga_cademica_detallesjefatura");

            entity.Property(e => e.autotimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.fecha_asignacion_carga).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_docente).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_fechas_carga).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_jefatura).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.no_oficio)
                .HasMaxLength(20)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.token).HasMaxLength(255);
            entity.Property(e => e.total_horas_apoyo).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.total_horas_frente_grupo).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_docenteNavigation).WithMany(p => p.carga_academicas)
                .HasForeignKey(d => d.id_docente)
                .HasConstraintName("carga_academica$FK_carga_cademica_detallesdocente");

            entity.HasOne(d => d.id_fechas_cargaNavigation).WithMany(p => p.carga_academicas)
                .HasForeignKey(d => d.id_fechas_carga)
                .HasConstraintName("carga_academica$FK_carga_academica_fechas_carga");

            entity.HasOne(d => d.id_jefaturaNavigation).WithMany(p => p.carga_academicas)
                .HasForeignKey(d => d.id_jefatura)
                .HasConstraintName("carga_academica$FK_carga_cademica_detallesjefatura");
        });

        modelBuilder.Entity<carrera>(entity =>
        {
            entity.HasKey(e => e.id_carrera).HasName("PK_carreras_id_carrera");

            entity.ToTable("carreras", "adedb");

            entity.Property(e => e.abreviatura).HasMaxLength(20);
            entity.Property(e => e.clave_estudios).HasMaxLength(13);
            entity.Property(e => e.modalidad).HasMaxLength(25);
            entity.Property(e => e.nivel_estudios).HasMaxLength(15);
            entity.Property(e => e.nombreCarrera).HasMaxLength(100);
        });

        modelBuilder.Entity<correos_recuperacion>(entity =>
        {
            entity.HasKey(e => e.id_correo_recovery).HasName("PK_correos_recuperacion_id_correo_recovery");

            entity.ToTable("correos_recuperacion", "adedb");

            entity.Property(e => e.clave).HasMaxLength(300);
            entity.Property(e => e.email).HasMaxLength(300);
            entity.Property(e => e.fecha)
                .HasPrecision(0)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<detallesadministrativo>(entity =>
        {
            entity.HasKey(e => e.id_administrativos).HasName("PK_detallesadministrativos_id_administrativos");

            entity.ToTable("detallesadministrativos", "adedb");

            entity.HasIndex(e => e.id_persona, "FK_detalles_administrativo_persona");

            entity.Property(e => e.nivel_estudios).HasMaxLength(50);
            entity.Property(e => e.puesto).HasMaxLength(80);

            entity.HasOne(d => d.id_personaNavigation).WithMany(p => p.detallesadministrativos)
                .HasForeignKey(d => d.id_persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesadministrativos$FK_detalles_administrativo_persona");
        });

        modelBuilder.Entity<detallesdocente>(entity =>
        {
            entity.HasKey(e => e.id_docente).HasName("PK_detallesdocente_id_docente");

            entity.ToTable("detallesdocente", "adedb");

            entity.HasIndex(e => e.id_persona, "FK_detalles_docente_persona");

            entity.HasIndex(e => e.id_carrera, "fk_Carrera");

            entity.Property(e => e.nivel_estudios).HasMaxLength(15);
            entity.Property(e => e.tipo_clave).HasMaxLength(30);

            entity.HasOne(d => d.id_carreraNavigation).WithMany(p => p.detallesdocentes)
                .HasForeignKey(d => d.id_carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesdocente$fk_Carrera");

            entity.HasOne(d => d.id_personaNavigation).WithMany(p => p.detallesdocentes)
                .HasForeignKey(d => d.id_persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesdocente$FK_detalles_docente_persona");
        });

        modelBuilder.Entity<detallesestudiante>(entity =>
        {
            entity.HasKey(e => e.id_academicos).HasName("PK_detallesestudiante_id_academicos");

            entity.ToTable("detallesestudiante", "adedb");

            entity.HasIndex(e => e.id_carrera, "FK_detalles_estudiante_carrera");

            entity.HasIndex(e => e.id_persona, "FK_detalles_estudiantes_persona");

            entity.HasIndex(e => e.id_grupo, "FK_detallesestudiantes_grupo");

            entity.Property(e => e.generacion).HasMaxLength(25);
            entity.Property(e => e.matricula).HasMaxLength(10);
            entity.Property(e => e.semestre).HasMaxLength(2);

            entity.HasOne(d => d.id_carreraNavigation).WithMany(p => p.detallesestudiantes)
                .HasForeignKey(d => d.id_carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesestudiante$FK_detalles_estudiante_carrera");

            entity.HasOne(d => d.id_grupoNavigation).WithMany(p => p.detallesestudiantes)
                .HasForeignKey(d => d.id_grupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesestudiante$id_grupo");

            entity.HasOne(d => d.id_personaNavigation).WithMany(p => p.detallesestudiantes)
                .HasForeignKey(d => d.id_persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesestudiante$FK_detalles_estudiantes_persona");
        });

        modelBuilder.Entity<detallesjefatura>(entity =>
        {
            entity.HasKey(e => e.id_jefatura).HasName("PK_detallesjefatura_id_jefatura");

            entity.ToTable("detallesjefatura", "adedb");

            entity.HasIndex(e => e.id_carrera, "FK_jefatura_carrera");

            entity.HasIndex(e => e.id_persona, "FK_jefatura_persona");

            entity.Property(e => e.nivel_estudios).HasMaxLength(100);

            entity.HasOne(d => d.id_carreraNavigation).WithMany(p => p.detallesjefaturas)
                .HasForeignKey(d => d.id_carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesjefatura$FK_jefatura_carrera");

            entity.HasOne(d => d.id_personaNavigation).WithMany(p => p.detallesjefaturas)
                .HasForeignKey(d => d.id_persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallesjefatura$FK_jefatura_persona");
        });

        modelBuilder.Entity<fechas_carga>(entity =>
        {
            entity.HasKey(e => e.id_fechas_carga).HasName("PK_fechas_carga_id_fechas_carga");

            entity.ToTable("fechas_carga", "adedb");

            entity.Property(e => e.ano_reinscripcion).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.autotimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.fecha_acta_final).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.fecha_planeacion).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.fecha_reporte1).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.fecha_reporte2).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.fecha_reporte3).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.periodo_carga)
                .HasMaxLength(30)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.periodo_final).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.periodo_inicial).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.status_f)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<grupo>(entity =>
        {
            entity.HasKey(e => e.id_grupo).HasName("PK_grupos_id_grupo");

            entity.ToTable("grupos", "adedb");

            entity.HasIndex(e => e.id_carrera, "FK_grupos_carrera");

            entity.HasIndex(e => e.token, "grupos$uniq_token").IsUnique();

            entity.Property(e => e.nombreClave).HasMaxLength(20);
            entity.Property(e => e.token)
                .HasMaxLength(120)
                .HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_carreraNavigation).WithMany(p => p.grupos)
                .HasForeignKey(d => d.id_carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grupos$FK_grupos_carrera");
        });

        modelBuilder.Entity<horario>(entity =>
        {
            entity.HasKey(e => e.id_horario).HasName("PK_horario_id_horario");

            entity.ToTable("horario", "adedb");

            entity.HasIndex(e => e.id_aula, "FK_id_aula");

            entity.HasIndex(e => e.id_mate_asignatura, "FK_id_mate_asignatura");

            entity.Property(e => e.dia).HasMaxLength(12);
            entity.Property(e => e.hora_final).HasMaxLength(15);
            entity.Property(e => e.hora_inicial).HasMaxLength(15);

            entity.HasOne(d => d.id_aulaNavigation).WithMany(p => p.horarios)
                .HasForeignKey(d => d.id_aula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("horario$FK_id_aula");

            entity.HasOne(d => d.id_mate_asignaturaNavigation).WithMany(p => p.horarios)
                .HasForeignKey(d => d.id_mate_asignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("horario$FK_id_mate_asignatura");
        });

        modelBuilder.Entity<horario_apoyo>(entity =>
        {
            entity.HasKey(e => e.id_horario_apoyo).HasName("PK_horario_apoyo_id_horario_apoyo");

            entity.ToTable("horario_apoyo", "adedb");

            entity.HasIndex(e => e.id_apoyo_docencia, "FK_apoyo_docencia_horario_apoyo");

            entity.HasIndex(e => e.id_aula, "FK_aula_horario_apoyo");

            entity.Property(e => e.dia_apoyo)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.h_apoyo_final)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.h_apoyo_inicial)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_apoyo_docencia).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_aula).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_apoyo_docenciaNavigation).WithMany(p => p.horario_apoyos)
                .HasForeignKey(d => d.id_apoyo_docencia)
                .HasConstraintName("horario_apoyo$FK_apoyo_docencia_horario_apoyo");

            entity.HasOne(d => d.id_aulaNavigation).WithMany(p => p.horario_apoyos)
                .HasForeignKey(d => d.id_aula)
                .HasConstraintName("horario_apoyo$FK_aula_horario_apoyo");
        });

        modelBuilder.Entity<horario_tutoria>(entity =>
        {
            entity.HasKey(e => e.id_horario_tutoria).HasName("PK_horario_tutorias_id_horario_tutoria");

            entity.ToTable("horario_tutorias", "adedb");

            entity.HasIndex(e => e.id_aula, "FK_aula_horario_tutorias");

            entity.HasIndex(e => e.id_tutorgrupo, "FK_tutogrupo_horario_tutorias");

            entity.Property(e => e.dia_tutoria)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.h_apoyo_final)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.h_apoyo_inicial)
                .HasMaxLength(10)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_aula).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_tutorgrupo).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_aulaNavigation).WithMany(p => p.horario_tutoria)
                .HasForeignKey(d => d.id_aula)
                .HasConstraintName("horario_tutorias$FK_aula_horario_tutorias");

            entity.HasOne(d => d.id_tutorgrupoNavigation).WithMany(p => p.horario_tutoria)
                .HasForeignKey(d => d.id_tutorgrupo)
                .HasConstraintName("horario_tutorias$FK_tutogrupo_horario_tutorias");
        });

        modelBuilder.Entity<materias_asignada>(entity =>
        {
            entity.HasKey(e => e.id_mate_asignada).HasName("PK_materias_asignadas_id_mate_asignada");

            entity.ToTable("materias_asignadas", "adedb");

            entity.HasIndex(e => e.id_asignatura, "FK_mate_asignadas_asignatura");

            entity.HasIndex(e => e.id_carga_academica, "FK_mate_asignadas_ca");

            entity.HasIndex(e => e.id_grupo, "FK_materias_asignadas_grupos");

            entity.Property(e => e.autotimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.id_asignatura).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.id_carga_academica).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_asignaturaNavigation).WithMany(p => p.materias_asignada)
                .HasForeignKey(d => d.id_asignatura)
                .HasConstraintName("materias_asignadas$FK_mate_asignadas_asignatura");

            entity.HasOne(d => d.id_carga_academicaNavigation).WithMany(p => p.materias_asignada)
                .HasForeignKey(d => d.id_carga_academica)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("materias_asignadas$materias_asignadas_ibfk_1");

            entity.HasOne(d => d.id_grupoNavigation).WithMany(p => p.materias_asignada)
                .HasForeignKey(d => d.id_grupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("materias_asignadas$FK_materias_asignadas_grupos");
        });

        modelBuilder.Entity<materias_cursar>(entity =>
        {
            entity.HasKey(e => e.id_materiasCursadas).HasName("PK_materias_cursar_id_materiasCursadas");

            entity.ToTable("materias_cursar", "adedb");

            entity.HasIndex(e => e.id_mate_asignada, "FK_materias_cursar_mate_asignada");

            entity.HasIndex(e => e.id_re_inscripcion, "id_re_inscripcion");

            entity.Property(e => e.id_mate_asignada).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.tipo_curso).HasMaxLength(15);

            entity.HasOne(d => d.id_mate_asignadaNavigation).WithMany(p => p.materias_cursars)
                .HasForeignKey(d => d.id_mate_asignada)
                .HasConstraintName("materias_cursar$FK_materias_cursar_mate_asignada");

            entity.HasOne(d => d.id_re_inscripcionNavigation).WithMany(p => p.materias_cursars)
                .HasForeignKey(d => d.id_re_inscripcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("materias_cursar$materias_cursar_ibfk_2");
        });

        modelBuilder.Entity<media_superior>(entity =>
        {
            entity.HasKey(e => e.id_mediaSuperior).HasName("PK_media_superior_id_mediaSuperior");

            entity.ToTable("media_superior", "adedb");

            entity.Property(e => e.subsistemaEducativoM).HasMaxLength(200);
        });

        modelBuilder.Entity<nombremediasuperior>(entity =>
        {
            entity.HasKey(e => e.id_nombremedia_superior).HasName("PK_nombremediasuperior_id_nombremedia_superior");

            entity.ToTable("nombremediasuperior", "adedb");

            entity.HasIndex(e => e.id_mediaSuperior, "fk_mediaSuperior");

            entity.Property(e => e.nombreMediaSuperior1)
                .HasMaxLength(100)
                .HasColumnName("nombreMediaSuperior");

            entity.HasOne(d => d.id_mediaSuperiorNavigation).WithMany(p => p.nombremediasuperiors)
                .HasForeignKey(d => d.id_mediaSuperior)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nombremediasuperior$fk_mediaSuperior");
        });

        modelBuilder.Entity<notificacione>(entity =>
        {
            entity.HasKey(e => e.id_notificacion).HasName("PK_notificaciones_id_notificacion");

            entity.ToTable("notificaciones", "adedb");

            entity.HasIndex(e => e.id_destinatario, "FK_id_destinatario_persona");

            entity.HasIndex(e => e.id_remitente, "FK_id_remitente_persona");

            entity.Property(e => e.descripcion).HasMaxLength(255);
            entity.Property(e => e.fecha_envio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.status).HasMaxLength(20);
            entity.Property(e => e.titulo).HasMaxLength(100);
            entity.Property(e => e.token).HasMaxLength(255);

            entity.HasOne(d => d.id_destinatarioNavigation).WithMany(p => p.notificacioneid_destinatarioNavigations)
                .HasForeignKey(d => d.id_destinatario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones$FK_id_destinatario_persona");

            entity.HasOne(d => d.id_remitenteNavigation).WithMany(p => p.notificacioneid_remitenteNavigations)
                .HasForeignKey(d => d.id_remitente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones$FK_id_remitente_persona");
        });

        modelBuilder.Entity<pago>(entity =>
        {
            entity.HasKey(e => e.id_pagos).HasName("PK_pagos_id_pagos");

            entity.ToTable("pagos", "adedb");

            entity.HasIndex(e => e.id_estudiante, "FK_pagos_persona");

            entity.HasIndex(e => e.id_DSA, "FK_pagos_persona_DSA");

            entity.Property(e => e.nombreArchivo).HasMaxLength(100);
            entity.Property(e => e.statusValidacion).HasMaxLength(80);
        });

        modelBuilder.Entity<persona>(entity =>
        {
            entity.HasKey(e => e.id_persona).HasName("PK_persona_id_persona");

            entity.ToTable("persona", "adedb");

            entity.Property(e => e.a_materno).HasMaxLength(25);
            entity.Property(e => e.a_paterno).HasMaxLength(25);
            entity.Property(e => e.calle).HasMaxLength(60);
            entity.Property(e => e.ciudad).HasMaxLength(40);
            entity.Property(e => e.colonia).HasMaxLength(55);
            entity.Property(e => e.contrasena).HasMaxLength(50);
            entity.Property(e => e.correo_inst).HasMaxLength(36);
            entity.Property(e => e.curp).HasMaxLength(18);
            entity.Property(e => e.estado).HasMaxLength(25);
            entity.Property(e => e.estadoCivil).HasMaxLength(100);
            entity.Property(e => e.fechaNcimiento).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.municipio).HasMaxLength(55);
            entity.Property(e => e.nombre).HasMaxLength(25);
            entity.Property(e => e.observacion).HasMaxLength(200);
            entity.Property(e => e.rfc).HasMaxLength(13);
            entity.Property(e => e.status).HasMaxLength(8);
            entity.Property(e => e.telefono).HasMaxLength(10);
        });

        modelBuilder.Entity<recuperacion>(entity =>
        {
            entity.HasKey(e => e.id_recuperacion).HasName("PK_recuperacion_id_recuperacion");

            entity.ToTable("recuperacion", "adedb");

            entity.Property(e => e.correo).HasMaxLength(45);
            entity.Property(e => e.token).HasMaxLength(100);
        });

        modelBuilder.Entity<roles_usuario>(entity =>
        {
            entity.HasKey(e => e.id_rol).HasName("PK_roles_usuarios_id_rol");

            entity.ToTable("roles_usuarios", "adedb");

            entity.Property(e => e.rol).HasMaxLength(15);
        });

        modelBuilder.Entity<solicitud_inscripcion>(entity =>
        {
            entity.HasKey(e => e.id_solicitud_inscripcion).HasName("PK_solicitud_inscripcion_id_solicitud_inscripcion");

            entity.ToTable("solicitud_inscripcion", "adedb");

            entity.HasIndex(e => e.id_nombremedia_superior, "FK_nombremediasuperior");

            entity.HasIndex(e => e.id_academicos, "FK_solicitud_inscripcion_detallesestudiante");

            entity.Property(e => e.promedio_mediasup).HasMaxLength(10);
            entity.Property(e => e.token).HasMaxLength(50);

            entity.HasOne(d => d.id_academicosNavigation).WithMany(p => p.solicitud_inscripcions)
                .HasForeignKey(d => d.id_academicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_inscripcion$solicitud_inscripcion_ibfk_2");

            entity.HasOne(d => d.id_nombremedia_superiorNavigation).WithMany(p => p.solicitud_inscripcions)
                .HasForeignKey(d => d.id_nombremedia_superior)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_inscripcion$solicitud_inscripcion_ibfk_1");
        });

        modelBuilder.Entity<solicitud_re_inscripcion>(entity =>
        {
            entity.HasKey(e => e.id_re_inscripcion).HasName("PK_solicitud_re_inscripcion_id_re_inscripcion");

            entity.ToTable("solicitud_re_inscripcion", "adedb");

            entity.HasIndex(e => e.id_carrera, "FK_solicitud_reinscripcion_carrera");

            entity.HasIndex(e => e.id_jefatura, "FK_solicitud_reinscripcion_persona");

            entity.HasIndex(e => e.id_academicos, "fk_solicitud_re_inscripcion_detallesestudiante");

            entity.HasIndex(e => e.id_fechas_carga, "fk_solicitud_re_inscripcion_fecha");

            entity.Property(e => e.semestre_re_inscripcion).HasMaxLength(12);
            entity.Property(e => e.status_SR).HasMaxLength(15);
            entity.Property(e => e.status_inscripcion).HasMaxLength(15);
            entity.Property(e => e.token).HasMaxLength(255);
            entity.Property(e => e.turno).HasMaxLength(16);

            entity.HasOne(d => d.id_academicosNavigation).WithMany(p => p.solicitud_re_inscripcions)
                .HasForeignKey(d => d.id_academicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_re_inscripcion$fk_solicitud_re_inscripcion_detallesestudiante");

            entity.HasOne(d => d.id_carreraNavigation).WithMany(p => p.solicitud_re_inscripcions)
                .HasForeignKey(d => d.id_carrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_re_inscripcion$solicitud_re_inscripcion_ibfk_3");

            entity.HasOne(d => d.id_fechas_cargaNavigation).WithMany(p => p.solicitud_re_inscripcions)
                .HasForeignKey(d => d.id_fechas_carga)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_re_inscripcion$solicitud_re_inscripcion_ibfk_2");

            entity.HasOne(d => d.id_jefaturaNavigation).WithMany(p => p.solicitud_re_inscripcions)
                .HasForeignKey(d => d.id_jefatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitud_re_inscripcion$solicitud_re_inscripcion_ibfk_4");
        });

        modelBuilder.Entity<tutor>(entity =>
        {
            entity.HasKey(e => e.id_tutor).HasName("PK_tutor_id_tutor");

            entity.ToTable("tutor", "adedb");

            entity.HasIndex(e => e.id_persona, "FK_tutor_inst");

            entity.Property(e => e.a_maternoT).HasMaxLength(200);
            entity.Property(e => e.a_paternoT).HasMaxLength(200);
            entity.Property(e => e.calleT).HasMaxLength(200);
            entity.Property(e => e.ciudadT).HasMaxLength(40);
            entity.Property(e => e.codigo_postalT).HasMaxLength(200);
            entity.Property(e => e.coloniaT).HasMaxLength(200);
            entity.Property(e => e.estadoT).HasMaxLength(200);
            entity.Property(e => e.nombreT).HasMaxLength(200);
            entity.Property(e => e.telefonoT).HasMaxLength(200);

            entity.HasOne(d => d.id_personaNavigation).WithMany(p => p.tutors)
                .HasForeignKey(d => d.id_persona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tutor$tutor_ibfk_1");
        });

        modelBuilder.Entity<tutorgrupo>(entity =>
        {
            entity.HasKey(e => e.id_tutorgrupo).HasName("PK_tutorgrupo_id_tutorgrupo");

            entity.ToTable("tutorgrupo", "adedb");

            entity.HasIndex(e => e.id_carga_academica, "FK_carga_tutor");

            entity.HasIndex(e => e.id_grupo, "FK_grupo_tutor");

            entity.Property(e => e.id_carga_academica).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.id_carga_academicaNavigation).WithMany(p => p.tutorgrupos)
                .HasForeignKey(d => d.id_carga_academica)
                .HasConstraintName("tutorgrupo$tutorgrupo_ibfk_1");

            entity.HasOne(d => d.id_grupoNavigation).WithMany(p => p.tutorgrupos)
                .HasForeignKey(d => d.id_grupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tutorgrupo$tutorgrupo_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
