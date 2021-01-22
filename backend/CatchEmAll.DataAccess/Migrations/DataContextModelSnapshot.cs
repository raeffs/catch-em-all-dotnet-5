// <auto-generated />
using CatchEmAll.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CatchEmAll.Migrations
{
  [DbContext(typeof(DataContext))]
  partial class DataContextModelSnapshot : ModelSnapshot
  {
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .UseIdentityColumns()
          .HasAnnotation("Relational:MaxIdentifierLength", 128)
          .HasAnnotation("ProductVersion", "5.0.1");

      modelBuilder.Entity("CatchEmAll.Models.Auction", b =>
          {
            b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

            b.Property<Guid>("CategoryId")
                      .HasColumnType("uniqueidentifier");

            b.Property<Guid>("SellerId")
                      .HasColumnType("uniqueidentifier");

            b.HasKey("Id");

            b.HasIndex("CategoryId");

            b.HasIndex("SellerId");

            b.ToTable("Auctions");
          });

      modelBuilder.Entity("CatchEmAll.Models.Category", b =>
          {
            b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

            b.Property<string>("Name")
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnType("nvarchar(100)");

            b.HasKey("Id");

            b.ToTable("Categories");
          });

      modelBuilder.Entity("CatchEmAll.Models.SearchQuery", b =>
          {
            b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

            b.Property<string>("Name")
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnType("nvarchar(100)");

            b.Property<Guid?>("UserId")
                      .HasColumnType("uniqueidentifier");

            b.HasKey("Id");

            b.HasIndex("UserId");

            b.ToTable("SearchQueries");
          });

      modelBuilder.Entity("CatchEmAll.Models.SearchResult", b =>
          {
            b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

            b.Property<Guid>("AuctionId")
                      .HasColumnType("uniqueidentifier");

            b.Property<Guid>("QueryId")
                      .HasColumnType("uniqueidentifier");

            b.HasKey("Id");

            b.HasIndex("AuctionId");

            b.HasIndex("QueryId", "AuctionId")
                      .IsUnique();

            b.ToTable("SearchResults");
          });

      modelBuilder.Entity("CatchEmAll.Models.Seller", b =>
          {
            b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

            b.Property<string>("Name")
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnType("nvarchar(100)");

            b.HasKey("Id");

            b.ToTable("Sellers");
          });

      modelBuilder.Entity("CatchEmAll.Models.UserReference", b =>
          {
            b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

            b.Property<string>("ExternalId")
                      .IsRequired()
                      .HasMaxLength(64)
                      .HasColumnType("nvarchar(64)");

            b.HasKey("Id");

            b.ToTable("Users");
          });

      modelBuilder.Entity("CatchEmAll.Models.Auction", b =>
          {
            b.HasOne("CatchEmAll.Models.Category", "Category")
                      .WithMany()
                      .HasForeignKey("CategoryId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("CatchEmAll.Models.Seller", "Seller")
                      .WithMany()
                      .HasForeignKey("SellerId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.OwnsOne("CatchEmAll.Models.AuctionInfo", "Info", b1 =>
                      {
                    b1.Property<Guid>("AuctionId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<int>("Condition")
                              .HasColumnType("int");

                    b1.Property<DateTimeOffset>("Created")
                              .HasColumnType("datetimeoffset");

                    b1.Property<DateTimeOffset>("Ends")
                              .HasColumnType("datetimeoffset");

                    b1.Property<bool>("IsClosed")
                              .HasColumnType("bit");

                    b1.Property<bool>("IsSold")
                              .HasColumnType("bit");

                    b1.Property<string>("Name")
                              .IsRequired()
                              .HasMaxLength(100)
                              .HasColumnType("nvarchar(100)");

                    b1.Property<int>("Type")
                              .HasColumnType("int");

                    b1.HasKey("AuctionId");

                    b1.ToTable("Auctions");

                    b1.WithOwner()
                              .HasForeignKey("AuctionId");
                  });

            b.OwnsOne("CatchEmAll.Models.AuctionPrice", "Price", b1 =>
                      {
                    b1.Property<Guid>("AuctionId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<decimal?>("BidPrice")
                              .HasColumnType("decimal(18,6)");

                    b1.Property<decimal?>("FinalPrice")
                              .HasColumnType("decimal(18,6)");

                    b1.Property<int>("NumberOfBids")
                              .HasColumnType("int");

                    b1.Property<decimal?>("PurchasePrice")
                              .HasColumnType("decimal(18,6)");

                    b1.Property<decimal?>("StartPrice")
                              .HasColumnType("decimal(18,6)");

                    b1.HasKey("AuctionId");

                    b1.ToTable("Auctions");

                    b1.WithOwner()
                              .HasForeignKey("AuctionId");
                  });

            b.OwnsOne("CatchEmAll.Models.ProviderInfo", "Provider", b1 =>
                      {
                    b1.Property<Guid>("AuctionId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<string>("Key")
                              .IsRequired()
                              .HasMaxLength(64)
                              .HasColumnType("nvarchar(64)");

                    b1.Property<string>("Value")
                              .IsRequired()
                              .HasMaxLength(64)
                              .HasColumnType("nvarchar(64)");

                    b1.HasKey("AuctionId");

                    b1.HasIndex("Key", "Value")
                              .IsUnique();

                    b1.ToTable("Auctions");

                    b1.WithOwner()
                              .HasForeignKey("AuctionId");
                  });

            b.OwnsOne("CatchEmAll.Models.UpdateInfo", "Update", b1 =>
                      {
                    b1.Property<Guid>("AuctionId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<bool>("IsLocked")
                              .HasColumnType("bit");

                    b1.Property<DateTimeOffset>("LastAttempted")
                              .HasColumnType("datetimeoffset");

                    b1.Property<int>("NumberOfFailures")
                              .HasColumnType("int");

                    b1.Property<int>("NumberOfResets")
                              .HasColumnType("int");

                    b1.Property<DateTimeOffset>("Updated")
                              .HasColumnType("datetimeoffset");

                    b1.HasKey("AuctionId");

                    b1.ToTable("Auctions");

                    b1.WithOwner()
                              .HasForeignKey("AuctionId");
                  });

            b.Navigation("Category");

            b.Navigation("Info")
                      .IsRequired();

            b.Navigation("Price")
                      .IsRequired();

            b.Navigation("Provider")
                      .IsRequired();

            b.Navigation("Seller");

            b.Navigation("Update")
                      .IsRequired();
          });

      modelBuilder.Entity("CatchEmAll.Models.Category", b =>
          {
            b.OwnsOne("CatchEmAll.Models.ProviderInfo", "Provider", b1 =>
                      {
                    b1.Property<Guid>("CategoryId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<string>("Key")
                              .IsRequired()
                              .HasMaxLength(64)
                              .HasColumnType("nvarchar(64)");

                    b1.Property<string>("Value")
                              .IsRequired()
                              .HasMaxLength(64)
                              .HasColumnType("nvarchar(64)");

                    b1.HasKey("CategoryId");

                    b1.HasIndex("Key", "Value")
                              .IsUnique();

                    b1.ToTable("Categories");

                    b1.WithOwner()
                              .HasForeignKey("CategoryId");
                  });

            b.Navigation("Provider")
                      .IsRequired();
          });

      modelBuilder.Entity("CatchEmAll.Models.SearchQuery", b =>
          {
            b.HasOne("CatchEmAll.Models.UserReference", "User")
                      .WithMany()
                      .HasForeignKey("UserId");

            b.OwnsOne("CatchEmAll.Models.SearchCriteria", "Criteria", b1 =>
                      {
                    b1.Property<Guid>("SearchQueryId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<string>("WithAllTheseWords")
                              .IsRequired()
                              .HasMaxLength(100)
                              .HasColumnType("nvarchar(100)");

                    b1.Property<string>("WithExactlyTheseWords")
                              .IsRequired()
                              .HasMaxLength(100)
                              .HasColumnType("nvarchar(100)");

                    b1.Property<string>("WithNoneOfTheseWords")
                              .IsRequired()
                              .HasMaxLength(100)
                              .HasColumnType("nvarchar(100)");

                    b1.Property<string>("WithOneOfTheseWords")
                              .IsRequired()
                              .HasMaxLength(100)
                              .HasColumnType("nvarchar(100)");

                    b1.HasKey("SearchQueryId");

                    b1.ToTable("SearchQueries");

                    b1.WithOwner()
                              .HasForeignKey("SearchQueryId");
                  });

            b.OwnsOne("CatchEmAll.Models.SearchSettings", "Settings", b1 =>
                      {
                    b1.Property<Guid>("SearchQueryId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<bool>("AutoFilterDeletedDuplicates")
                              .HasColumnType("bit");

                    b1.Property<int>("Priority")
                              .HasColumnType("int");

                    b1.HasKey("SearchQueryId");

                    b1.ToTable("SearchQueries");

                    b1.WithOwner()
                              .HasForeignKey("SearchQueryId");
                  });

            b.OwnsOne("CatchEmAll.Models.UpdateInfo", "Update", b1 =>
                      {
                    b1.Property<Guid>("SearchQueryId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<bool>("IsLocked")
                              .HasColumnType("bit");

                    b1.Property<DateTimeOffset>("LastAttempted")
                              .HasColumnType("datetimeoffset");

                    b1.Property<int>("NumberOfFailures")
                              .HasColumnType("int");

                    b1.Property<int>("NumberOfResets")
                              .HasColumnType("int");

                    b1.Property<DateTimeOffset>("Updated")
                              .HasColumnType("datetimeoffset");

                    b1.HasKey("SearchQueryId");

                    b1.ToTable("SearchQueries");

                    b1.WithOwner()
                              .HasForeignKey("SearchQueryId");
                  });

            b.Navigation("Criteria")
                      .IsRequired();

            b.Navigation("Settings")
                      .IsRequired();

            b.Navigation("Update")
                      .IsRequired();

            b.Navigation("User");
          });

      modelBuilder.Entity("CatchEmAll.Models.SearchResult", b =>
          {
            b.HasOne("CatchEmAll.Models.Auction", "Auction")
                      .WithMany("Results")
                      .HasForeignKey("AuctionId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("CatchEmAll.Models.SearchQuery", "Query")
                      .WithMany("Results")
                      .HasForeignKey("QueryId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.OwnsOne("CatchEmAll.Models.LifetimeInfo", "Lifetime", b1 =>
                      {
                    b1.Property<Guid>("SearchResultId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<DateTimeOffset>("Created")
                              .HasColumnType("datetimeoffset");

                    b1.Property<DateTimeOffset?>("Deleted")
                              .HasColumnType("datetimeoffset");

                    b1.Property<bool>("IsDeleted")
                              .HasColumnType("bit");

                    b1.HasKey("SearchResultId");

                    b1.ToTable("SearchResults");

                    b1.WithOwner()
                              .HasForeignKey("SearchResultId");
                  });

            b.Navigation("Auction");

            b.Navigation("Lifetime")
                      .IsRequired();

            b.Navigation("Query");
          });

      modelBuilder.Entity("CatchEmAll.Models.Seller", b =>
          {
            b.OwnsOne("CatchEmAll.Models.ProviderInfo", "Provider", b1 =>
                      {
                    b1.Property<Guid>("SellerId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<string>("Key")
                              .IsRequired()
                              .HasMaxLength(64)
                              .HasColumnType("nvarchar(64)");

                    b1.Property<string>("Value")
                              .IsRequired()
                              .HasMaxLength(64)
                              .HasColumnType("nvarchar(64)");

                    b1.HasKey("SellerId");

                    b1.HasIndex("Key", "Value")
                              .IsUnique();

                    b1.ToTable("Sellers");

                    b1.WithOwner()
                              .HasForeignKey("SellerId");
                  });

            b.Navigation("Provider")
                      .IsRequired();
          });

      modelBuilder.Entity("CatchEmAll.Models.UserReference", b =>
          {
            b.OwnsOne("CatchEmAll.Models.UserSettings", "Settings", b1 =>
                      {
                    b1.Property<Guid>("UserReferenceId")
                              .HasColumnType("uniqueidentifier");

                    b1.Property<string>("EmailAddress")
                              .IsRequired()
                              .HasMaxLength(100)
                              .HasColumnType("nvarchar(100)");

                    b1.HasKey("UserReferenceId");

                    b1.ToTable("Users");

                    b1.WithOwner()
                              .HasForeignKey("UserReferenceId");
                  });

            b.Navigation("Settings")
                      .IsRequired();
          });

      modelBuilder.Entity("CatchEmAll.Models.Auction", b =>
          {
            b.Navigation("Results");
          });

      modelBuilder.Entity("CatchEmAll.Models.SearchQuery", b =>
          {
            b.Navigation("Results");
          });
#pragma warning restore 612, 618
    }
  }
}
