using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public  class QuestionDbContext : DbContext
    {

        public QuestionDbContext( DbContextOptions < QuestionDbContext > options ) : base ( options)
        {
        }


        public DbSet < DocxFile> DocxFiles { get; set; }
        public DbSet < Options> Options { get; set; }
        public DbSet < Questions> Questions { get; set; }
        public DbSet < User> Users { get; set; }


    }

    }
