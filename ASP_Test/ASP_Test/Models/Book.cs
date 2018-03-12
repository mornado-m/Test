//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using WebGrease.Css.Extensions;

namespace ASP_Test.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Sales = new HashSet<Sale>();
        }

        public Book Clone()
        {
            return new Book
            {
                book_id = this.book_id,
                book_name = this.book_name,
                author_id = this.author_id,
                available_count = this.available_count,
                price = this.price,
                description = this.description,
                Author = this.Author.Clone()
            };
        }
    
        public int book_id { get; set; }
        public string book_name { get; set; }
        public int author_id { get; set; }
        public int available_count { get; set; }
        public double price { get; set; }
        public string description { get; set; }
    
        public virtual Author Author { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
