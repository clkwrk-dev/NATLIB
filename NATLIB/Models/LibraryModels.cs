using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace NATLIB.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime? DatePublished { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string ISBN { get; set; }

        public int Edition { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Availability { get; set; }

        [Required]
        public string Summary { get; set; }
    }

    public class Magazine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Creator { get; set; }

        [Required]
        public DateTime? CoverDate { get; set; }

        [Required]
        public string Publisher { get; set; }

        public string ContentRating { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Availability { get; set; }
    }

    public class Newspaper
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Creator { get; set; }

        [Required]
        public DateTime? CoverDate { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Availability { get; set; }
    }

    public class GovernmentPublication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime? DatePublished { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Availability { get; set; }
    }

    public class OlaLeafManuscript
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Availability { get; set; }
    }
}