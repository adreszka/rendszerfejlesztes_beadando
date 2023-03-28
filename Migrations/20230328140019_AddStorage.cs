﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rendszerfejlesztes_beadando.Migrations
{
    /// <inheritdoc />
    public partial class AddStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Storage (Id) VALUES " +
                $"(111), (112), (113), (114), (115), (116), " +
                $"(121), (122), (123), (124), (125), (126), " +
                $"(131), (132), (133), (134), (135), (136), " +
                $"(141), (142), (143), (144), (145), (146), " +
                $"(211), (212), (213), (214), (215), (216), " +
                $"(221), (222), (223), (224), (225), (226), " +
                $"(231), (232), (233), (234), (235), (236), " +
                $"(241), (242), (243), (244), (245), (246), " +
                $"(311), (312), (313), (314), (315), (316), " +
                $"(321), (322), (323), (324), (325), (326), " +
                $"(331), (332), (333), (334), (335), (336), " +
                $"(341), (342), (343), (344), (345), (346), " +
                $"(411), (412), (413), (414), (415), (416), " +
                $"(421), (422), (423), (424), (425), (426), " +
                $"(431), (432), (433), (434), (435), (436), " +
                $"(441), (442), (443), (444), (445), (446), " +
                $"(511), (512), (513), (514), (515), (516), " +
                $"(521), (522), (523), (524), (525), (526), " +
                $"(531), (532), (533), (534), (535), (536), " +
                $"(541), (542), (543), (544), (545), (546), " +
                $"(611), (612), (613), (614), (615), (616), " +
                $"(621), (622), (623), (624), (625), (626), " +
                $"(631), (632), (633), (634), (635), (636), " +
                $"(641), (642), (643), (644), (645), (646), " +
                $"(711), (712), (713), (714), (715), (716), " +
                $"(721), (722), (723), (724), (725), (726), " +
                $"(731), (732), (733), (734), (735), (736), " +
                $"(741), (742), (743), (744), (745), (746), " +
                $"(811), (812), (813), (814), (815), (816), " +
                $"(821), (822), (823), (824), (825), (826), " +
                $"(831), (832), (833), (834), (835), (836), " +
                $"(841), (842), (843), (844), (845), (846), " +
                $"(911), (912), (913), (914), (915), (916), " +
                $"(921), (922), (923), (924), (925), (926), " +
                $"(931), (932), (933), (934), (935), (936), " +
                $"(941), (942), (943), (944), (945), (946), " +
                $"(1011), (1012), (1013), (1014), (1015), (1016), " +
                $"(1021), (1022), (1023), (1024), (1025), (1026), " +
                $"(1031), (1032), (1033), (1034), (1035), (1036), " +
                $"(1041), (1042), (1043), (1044), (1045), (1046)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}