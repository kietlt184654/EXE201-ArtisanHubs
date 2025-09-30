using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtisanHubs.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    avatar = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'active'::character varying"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("account_pkey", x => x.account_id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'active'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_pkey", x => x.category_id);
                    table.ForeignKey(
                        name: "category_parent_id_fkey",
                        column: x => x.parent_id,
                        principalTable: "category",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "workshoppackage",
                columns: table => new
                {
                    package_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    max_viewers = table.Column<int>(type: "integer", nullable: true),
                    commission_rate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'active'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("workshoppackage_pkey", x => x.package_id);
                });

            migrationBuilder.CreateTable(
                name: "artistprofile",
                columns: table => new
                {
                    artist_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<int>(type: "integer", nullable: false),
                    artist_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    shop_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    profile_image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    social_links = table.Column<string>(type: "text", nullable: true),
                    verified_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'unverified'::character varying"),
                    avg_rating = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: true, defaultValueSql: "0.00"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("artistprofile_pkey", x => x.artist_id);
                    table.ForeignKey(
                        name: "artistprofile_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<int>(type: "integer", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    shipping_address = table.Column<string>(type: "text", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'pending'::character varying"),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_pkey", x => x.order_id);
                    table.ForeignKey(
                        name: "Order_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "artistwallet",
                columns: table => new
                {
                    wallet_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    artist_id = table.Column<int>(type: "integer", nullable: false),
                    balance = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValueSql: "0.00"),
                    pending_balance = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValueSql: "0.00"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("artistwallet_pkey", x => x.wallet_id);
                    table.ForeignKey(
                        name: "artistwallet_artist_id_fkey",
                        column: x => x.artist_id,
                        principalTable: "artistprofile",
                        principalColumn: "artist_id");
                });

            migrationBuilder.CreateTable(
                name: "artistworkshop",
                columns: table => new
                {
                    workshop_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    artist_id = table.Column<int>(type: "integer", nullable: false),
                    package_id = table.Column<int>(type: "integer", nullable: false),
                    topic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'scheduled'::character varying"),
                    viewer_count = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    revenue = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true, defaultValueSql: "0.00")
                },
                constraints: table =>
                {
                    table.PrimaryKey("artistworkshop_pkey", x => x.workshop_id);
                    table.ForeignKey(
                        name: "artistworkshop_artist_id_fkey",
                        column: x => x.artist_id,
                        principalTable: "artistprofile",
                        principalColumn: "artist_id");
                    table.ForeignKey(
                        name: "artistworkshop_package_id_fkey",
                        column: x => x.package_id,
                        principalTable: "workshoppackage",
                        principalColumn: "package_id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    artist_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    story = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    discount_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    stock_quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    images = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'available'::character varying"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pkey", x => x.product_id);
                    table.ForeignKey(
                        name: "product_artist_id_fkey",
                        column: x => x.artist_id,
                        principalTable: "artistprofile",
                        principalColumn: "artist_id");
                    table.ForeignKey(
                        name: "product_category_id_fkey",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "withdrawrequest",
                columns: table => new
                {
                    withdraw_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    artist_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    bank_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    account_holder = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    account_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'pending'::character varying"),
                    requested_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    approved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    paid_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("withdrawrequest_pkey", x => x.withdraw_id);
                    table.ForeignKey(
                        name: "withdrawrequest_artist_id_fkey",
                        column: x => x.artist_id,
                        principalTable: "artistprofile",
                        principalColumn: "artist_id");
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: true),
                    workshop_id = table.Column<int>(type: "integer", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'completed'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_pkey", x => x.payment_id);
                    table.ForeignKey(
                        name: "payment_order_id_fkey",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "payment_workshop_id_fkey",
                        column: x => x.workshop_id,
                        principalTable: "artistworkshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "commission",
                columns: table => new
                {
                    commission_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    workshop_id = table.Column<int>(type: "integer", nullable: true),
                    artist_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    rate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    admin_share = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("commission_pkey", x => x.commission_id);
                    table.ForeignKey(
                        name: "commission_artist_id_fkey",
                        column: x => x.artist_id,
                        principalTable: "artistprofile",
                        principalColumn: "artist_id");
                    table.ForeignKey(
                        name: "commission_order_id_fkey",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "commission_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "commission_workshop_id_fkey",
                        column: x => x.workshop_id,
                        principalTable: "artistworkshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: true),
                    workshop_id = table.Column<int>(type: "integer", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("feedback_pkey", x => x.feedback_id);
                    table.ForeignKey(
                        name: "feedback_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "feedback_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "feedback_workshop_id_fkey",
                        column: x => x.workshop_id,
                        principalTable: "artistworkshop",
                        principalColumn: "workshop_id");
                });

            migrationBuilder.CreateTable(
                name: "orderdetail",
                columns: table => new
                {
                    order_detail_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orderdetail_pkey", x => x.order_detail_id);
                    table.ForeignKey(
                        name: "orderdetail_order_id_fkey",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "orderdetail_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "wallettransaction",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wallet_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    transaction_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    commission_id = table.Column<int>(type: "integer", nullable: true),
                    withdraw_id = table.Column<int>(type: "integer", nullable: true),
                    payment_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'completed'::character varying"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("wallettransaction_pkey", x => x.transaction_id);
                    table.ForeignKey(
                        name: "wallettransaction_commission_id_fkey",
                        column: x => x.commission_id,
                        principalTable: "commission",
                        principalColumn: "commission_id");
                    table.ForeignKey(
                        name: "wallettransaction_payment_id_fkey",
                        column: x => x.payment_id,
                        principalTable: "payment",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "wallettransaction_wallet_id_fkey",
                        column: x => x.wallet_id,
                        principalTable: "artistwallet",
                        principalColumn: "wallet_id");
                    table.ForeignKey(
                        name: "wallettransaction_withdraw_id_fkey",
                        column: x => x.withdraw_id,
                        principalTable: "withdrawrequest",
                        principalColumn: "withdraw_id");
                });

            migrationBuilder.CreateIndex(
                name: "account_email_key",
                table: "account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "account_username_key",
                table: "account",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "artistprofile_account_id_idx",
                table: "artistprofile",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "artistprofile_account_id_key",
                table: "artistprofile",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "artistwallet_artist_id_idx",
                table: "artistwallet",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "artistwallet_artist_id_key",
                table: "artistwallet",
                column: "artist_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "artistworkshop_artist_id_idx",
                table: "artistworkshop",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "artistworkshop_package_id_idx",
                table: "artistworkshop",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_parent_id",
                table: "category",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "commission_artist_id_idx",
                table: "commission",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "commission_order_id_idx",
                table: "commission",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_commission_product_id",
                table: "commission",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_commission_workshop_id",
                table: "commission",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "feedback_account_id_idx",
                table: "feedback",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "feedback_product_id_idx",
                table: "feedback",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "feedback_workshop_id_idx",
                table: "feedback",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "Order_account_id_idx",
                table: "Order",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "orderdetail_order_id_idx",
                table: "orderdetail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "orderdetail_product_id_idx",
                table: "orderdetail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "payment_order_id_idx",
                table: "payment",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "payment_workshop_id_idx",
                table: "payment",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "product_artist_id_idx",
                table: "product",
                column: "artist_id");

            migrationBuilder.CreateIndex(
                name: "product_category_id_idx",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallettransaction_commission_id",
                table: "wallettransaction",
                column: "commission_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallettransaction_payment_id",
                table: "wallettransaction",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallettransaction_withdraw_id",
                table: "wallettransaction",
                column: "withdraw_id");

            migrationBuilder.CreateIndex(
                name: "wallettransaction_wallet_id_idx",
                table: "wallettransaction",
                column: "wallet_id");

            migrationBuilder.CreateIndex(
                name: "withdrawrequest_artist_id_idx",
                table: "withdrawrequest",
                column: "artist_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedback");

            migrationBuilder.DropTable(
                name: "orderdetail");

            migrationBuilder.DropTable(
                name: "wallettransaction");

            migrationBuilder.DropTable(
                name: "commission");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "artistwallet");

            migrationBuilder.DropTable(
                name: "withdrawrequest");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "artistworkshop");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "artistprofile");

            migrationBuilder.DropTable(
                name: "workshoppackage");

            migrationBuilder.DropTable(
                name: "account");
        }
    }
}
